using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class WhichPeopleChosePair
    {
        static public IReadOnlyList<WhichPeopleChosePair> GetDefaultPeopleChosePairs()
        {
            List<WhichPeopleChosePair> pairs = new List<WhichPeopleChosePair>();

            pairs.Add(new WhichPeopleChosePair() { Value = (int)WhichPeopleChoseEnum.All, Text = "Всех" });
            pairs.Add(new WhichPeopleChosePair() { Value = (int)WhichPeopleChoseEnum.ValidCerts, Text = "С действительными справками" });
            pairs.Add(new WhichPeopleChosePair() { Value = (int)WhichPeopleChoseEnum.InvalidCerts, Text = "С недействительными справками" });

            return pairs;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
