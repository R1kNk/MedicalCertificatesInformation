using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ReportModels.Common
{
    public class PhysicalEducationStat<TEntity> where TEntity : class
    {

        public IReadOnlyList<RelatedStat<TEntity, PhysicalEducation>> PhysicalEducationStatistics { get;  set; }

        public int All { get; set; }

        public IReadOnlyList<int> CountsInEachStat
        {
            get
            {
                var list = new List<int>();
                foreach (var stat in PhysicalEducationStatistics)
                {
                    if (stat != null)
                        list.Add(stat.Count);
                    else list.Add(0);
                }
                return list;
            }
        }


        public PhysicalEducationStat(IReadOnlyList<PhysicalEducation> physicalEducations, List<List<TEntity>> stats)
        {
            var result = new List<RelatedStat<TEntity, PhysicalEducation>>();
            var counts = new List<int>();
            var lists = new List<List<TEntity>>();

            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i] == null || stats[i].Count == 0)
                {
                    All += 0;
                    counts.Add(0);
                    lists.Add(new List<TEntity>());
                }
                else
                {
                    All += stats[i].Count;
                    counts.Add(stats[i].Count);
                    lists.Add(stats[i]);
                }
            }
            for (int i = 0; i < lists.Count; i++)
            {
                var count = counts[i];
                var list = lists[i];
                var physicalEducation = physicalEducations[i];
                if (count == 0) result.Add(new RelatedStat<TEntity, PhysicalEducation>(0, 0, new List<TEntity>(), physicalEducations[i]));
                else
                {
                    var percentage = Math.Round((double)(100 * count) / All, 1);
                    result.Add(new RelatedStat<TEntity, PhysicalEducation>(count, percentage, list, physicalEducation));
                }
            }
            PhysicalEducationStatistics = result;

        }

        public PhysicalEducationStat(IReadOnlyList<PhysicalEducation> physicalEducations, List<List<TEntity>> stats, IReadOnlyList<int> countInEachList)
        {
            var result = new List<Stat<TEntity>>();
            var counts = new List<int>();
            var lists = new List<List<TEntity>>();

            for (int i = 0; i < countInEachList.Count; i++)
            {
                if (stats[i] == null)
                    result.Add(new RelatedStat<TEntity, PhysicalEducation>(0, 0, new List<TEntity>(), physicalEducations[i]));
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
                var physicalEducation = physicalEducations[i];
                if (count == 0) result.Add(new RelatedStat<TEntity, PhysicalEducation>(0, 0, new List<TEntity>(), physicalEducations[i]));
                else
                {
                    var percentage = Math.Round((double)(100 * count) / All, 1);
                    result.Add(new RelatedStat<TEntity, PhysicalEducation>(count, percentage, list, physicalEducation));
                }
            }


        }

    }
}
