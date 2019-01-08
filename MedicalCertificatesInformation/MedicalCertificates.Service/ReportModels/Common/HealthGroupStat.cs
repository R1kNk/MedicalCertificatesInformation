using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ReportModels.Common
{
    public class HealthGroupStat<TEntity> where  TEntity : class
    {
        public IReadOnlyList<RelatedStat<TEntity, HealthGroup>> HealthGroupStatistics { get; private set; }

        public int All { get; set; }

        public IReadOnlyList<int> CountsInEachStat
        {
            get {
                var list = new List<int>();
                foreach(var stat in HealthGroupStatistics)
                {
                    if (stat != null)
                        list.Add(stat.Count);
                    else list.Add(0);
                }
                return list;
            }
        }


        public HealthGroupStat(IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<IReadOnlyList<TEntity>> stats)
        {
            var result = new List<Stat<TEntity>>();
            var counts = new List<int>();
            var lists = new List<IReadOnlyList<TEntity>>();

            foreach (var list in stats)
            {
                if (list == null)
                    result.Add(new RelatedStat<TEntity, HealthGroup>());
                else
                {
                    All += list.Count;
                    counts.Add(list.Count);
                    lists.Add(list);
                }
            }
            for(int i = 0; i< lists.Count; i++)
            {
                var count = counts[i];
                var list = lists[i];
                var healthGroup = healthGroups[i];
                if (count == 0) result.Add(new RelatedStat<TEntity, HealthGroup>());
                else
                {
                    var percentage = Math.Round((double)(100 * count) / All, 1);
                    result.Add(new RelatedStat<TEntity, HealthGroup>(count, percentage, list, healthGroup));
                }
            }

        }

        public HealthGroupStat(IReadOnlyList<HealthGroup> healthGroups, IReadOnlyList<IReadOnlyList<TEntity>> stats, IReadOnlyList<int> countInEachList)
        {
            var result = new List<Stat<TEntity>>();
            var counts = new List<int>();
            var lists = new List<IReadOnlyList<TEntity>>();

            for(int i = 0; i< countInEachList.Count; i++)
            {
                if (stats[i] == null)
                    result.Add(new RelatedStat<TEntity, HealthGroup>());
                else
                {
                    All += countInEachList[i];
                    counts.Add(countInEachList[i]);
                    lists.Add(stats[i]);
                }
            }

            for (int i = 0; i < lists.Count; i++)
            {
                var count = counts[i];
                var list = lists[i];
                var healthGroup = healthGroups[i];
                if (count == 0) result.Add(new RelatedStat<TEntity, HealthGroup>());
                else
                {
                    var percentage = Math.Round((double)(100 * count) / All, 1);
                    result.Add(new RelatedStat<TEntity, HealthGroup>(count, percentage, list, healthGroup));
                }
            }

        }
    }
}
