using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ReportModels.Common
{
    public class RelatedStat<TListEntity, TRelatedEntity> : Stat<TListEntity> where TListEntity : class where TRelatedEntity : class, new()
    {
        public TRelatedEntity RelatedEntity { get; private set; }

        public RelatedStat(int count, double percentage, IReadOnlyList<TListEntity> entityList, TRelatedEntity relatedEntity)
            : base(count, percentage, entityList)
        {
            if (relatedEntity == null)
                RelatedEntity = new TRelatedEntity();
            else
            RelatedEntity = relatedEntity;
        }

        public RelatedStat() : base()
        {
            RelatedEntity = new TRelatedEntity();
        }
    }
}
