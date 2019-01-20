using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ReportTypePair
    {
        static public IReadOnlyList<ReportTypePair> GetDefaultReportTypePairs()
        {
            List<ReportTypePair> pairs = new List<ReportTypePair>();

            pairs.Add(new ReportTypePair() { Value = (int)ReportTypeEnum.AtTheMoment, Text = "На данный момент" });
            pairs.Add(new ReportTypePair() { Value = (int)ReportTypeEnum.OnADate, Text = "На определенную дату" });
            pairs.Add(new ReportTypePair() { Value = (int)ReportTypeEnum.WithinMonths, Text = "Через n месяцев" });

            return pairs;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
