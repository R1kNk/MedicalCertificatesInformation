using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class ReportSortPair
    {

        public static List<ReportSortPair> GetDefaultReportSortPair()
        {
            List<ReportSortPair> reportSortPairs = new List<ReportSortPair>();
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.BySurnameName, Text =  "По ФИО" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByGroup, Text = "По группе" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByDepartment, Text = "По отделению" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByHealthGroup, Text = "По группе здоровья" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByPhysicaleducationGroup, Text = "По группе по физкультуре" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByEndDateAscending, Text = "По окончанию справки (по возрастанию)" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByEndDateDescending, Text = "По окончанию справки (по убыванию)" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByCourseAscending, Text = "По курсу (по возрастанию)" });
            reportSortPairs.Add(new ReportSortPair() { Value = (int)ReportSortEnum.ByCourseDescending, Text = "По курсу (по убыванию)" });
            return reportSortPairs;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
