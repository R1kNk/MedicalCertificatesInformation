using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Common.ReportModels.Common
{
    class CertificatePresenceStat<TEntity> where TEntity : class 
    {
        public Tuple<int, double, IReadOnlyList<TEntity>> Have { get; private set; }
        public Tuple<int, double, IReadOnlyList<TEntity>> DontHave { get; private set; }

        public CertificatePresenceStat(IReadOnlyList<TEntity> have, IReadOnlyList<TEntity> dontHave)
        {
            int haveCount = default(int);
            int dontHaveCount = default(int);
            IReadOnlyList<TEntity> HaveList = new List<TEntity>();
            IReadOnlyList<TEntity> DontHaveList = new List<TEntity>();

            if (have != null)
            {
                haveCount = have.Count;
                HaveList = have;
            } 

            if (dontHave != null)
            {
                dontHaveCount = dontHave.Count;
                DontHaveList = dontHave;
            }

            int personCount = haveCount + dontHaveCount;
            if(personCount <=0)
            {
                Have = new Tuple<int, double, IReadOnlyList<TEntity>>(0, 0.0, HaveList);
                DontHave = new Tuple<int, double, IReadOnlyList<TEntity>>(0, 0.0, DontHaveList);
                return;
            }

            if (have == null) Have = new Tuple<int, double, IReadOnlyList<TEntity>>(0, 0.0, HaveList);
            if (dontHave == null) DontHave = new Tuple<int, double, IReadOnlyList<TEntity>>(0, 0.0, DontHaveList);


            double percentageHave = Math.Round((double)(100 * haveCount) / personCount, 1);
            double percentageDontHave = 100.0 - percentageHave;

            Have = new Tuple<int, double, IReadOnlyList<TEntity>>(haveCount, percentageHave, HaveList);
            DontHave = new Tuple<int, double, IReadOnlyList<TEntity>>(dontHaveCount, percentageHave,DontHaveList);
        }

       

    }
}
