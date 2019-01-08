﻿using System;
using System.Collections.Generic;

namespace MedicalCertificates.Common.ReportModels.Common
{
    public class CertificatePresenceStat<TEntity> where TEntity : class 
    {
        public Stat<TEntity> Have { get; private set; }
        public Stat<TEntity> DontHave { get; private set; }


        public IReadOnlyList<int> CountsInEachStat
        {
            get
            {
                var list = new List<int>();
                if (Have != null)
                    list.Add(Have.Count);
                else list.Add(0);
                if (DontHave != null)
                    list.Add(DontHave.Count);
                else list.Add(0);
                return list;
            }
        }

        public int All { get; set; }

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
                Have = new Stat<TEntity>(0, 0.0, HaveList);
                DontHave = new Stat<TEntity>(0, 0.0, DontHaveList);
                return;
            }

            if (have == null) Have = new Stat<TEntity>(0, 0.0, HaveList);
            if (dontHave == null) DontHave = new Stat<TEntity>(0, 0.0, DontHaveList);

            All = personCount;
            double percentageHave = Math.Round((double)(100 * haveCount) / personCount, 1);
            double percentageDontHave = 100.0 - percentageHave;

            Have = new Stat<TEntity>(haveCount, percentageHave, HaveList);
            DontHave = new Stat<TEntity>(dontHaveCount, percentageHave,DontHaveList);
        }

        public CertificatePresenceStat(IReadOnlyList<TEntity> have, IReadOnlyList<TEntity> dontHave, int haveCount, int dontHaveCount)
        {
            IReadOnlyList<TEntity> HaveList = new List<TEntity>();
            IReadOnlyList<TEntity> DontHaveList = new List<TEntity>();

            if (have != null)
            {
                HaveList = have;
            }

            if (dontHave != null)
            {
                DontHaveList = dontHave;
            }

            int personCount = haveCount + dontHaveCount;
            if (personCount <= 0)
            {
                Have = new Stat<TEntity>(0, 0.0, HaveList);
                DontHave = new Stat<TEntity>(0, 0.0, DontHaveList);
                return;
            }

            if (have == null) Have = new Stat<TEntity>(0, 0.0, HaveList);
            if (dontHave == null) DontHave = new Stat<TEntity>(0, 0.0, DontHaveList);

            All = personCount;
            double percentageHave = Math.Round((double)(100 * haveCount) / personCount, 1);
            double percentageDontHave = 100.0 - percentageHave;

            Have = new Stat<TEntity>(haveCount, percentageHave, HaveList);
            DontHave = new Stat<TEntity>(dontHaveCount, percentageHave, DontHaveList);
        }

    }
}
