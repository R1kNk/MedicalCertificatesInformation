using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;

namespace MedicalCertificatesInformation.Models.Database.Shared
{
    public class DropCreateIfEmpty<T> : IDatabaseInitializer<T> where T : DbContext
    {

        public void InitializeDatabase(T context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
                
            }
            if (dbExists)
            {
                string isDatabaseEmptyQuery = "SELECT SUM(s.row_count) from sys.tables t JOIN sys.dm_db_partition_stats s ON t.object_id = s.object_id AND t.type_desc = 'USER_TABLE' AND t.name not like '%Migration%' AND s.index_id IN (0,1)";

                Int64 RowsResult = context.Database.SqlQuery<Int64>(isDatabaseEmptyQuery).FirstOrDefault();
                if (RowsResult == 0)
                {
                    //string l = "while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY')) begin declare @sql nvarchar(2000) SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME + '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']') FROM information_schema.table_constraints WHERE CONSTRAINT_TYPE = 'FOREIGN KEY' exec (@sql) end";
                    //context.Database.ExecuteSqlCommand(l);
                    //context.Database.ExecuteSqlCommand("EXEC sp_MSforeachtable @command1 = \"DROP TABLE ?\"");
                    //var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                    //context.Database.ExecuteSqlCommand(dbCreationScript);
                    Seed(context);
                    context.SaveChanges();
                }         
            }
            else
            {
                context.Database.Create();
                Seed(context);
                context.SaveChanges();
            }

        }

        protected virtual void Seed(T context)
        {
        }

    }
}