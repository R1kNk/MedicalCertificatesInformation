using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Common.ReportModels.Common
{
    public class Stat<TEntity> where TEntity : class
    {
        public int Count { get; private set; }
        public double Percentage { get; private set; }
        public IReadOnlyList<TEntity> EntityList { get; private set; }

        public Stat(int count, double percentage, IReadOnlyList<TEntity> entityList)
        {
            Count = count;
            Percentage = percentage;
            EntityList = entityList;
        }
        public Stat()
        {
            Count = 0;
            Percentage = 0.0;
            EntityList = new List<TEntity>();
        }
    }
}
