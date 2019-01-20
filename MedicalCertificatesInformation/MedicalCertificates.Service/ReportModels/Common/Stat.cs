using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ReportModels.Common
{
    public class Stat<TEntity> where TEntity : class
    {
        public int Count { get; private set; }
        public double Percentage { get; private set; }
        public List<TEntity> EntityList { get;  set; }

        public Stat(int count, double percentage, List<TEntity> entityList)
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
