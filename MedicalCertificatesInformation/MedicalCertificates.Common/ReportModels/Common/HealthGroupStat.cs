using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Common.ReportModels.Common
{
    public class HealthGroupStat<TEntity> where  TEntity : class
    {
        public Tuple<int, double, IReadOnlyList<TEntity>> HaveList { get; private set; }
    }
}
