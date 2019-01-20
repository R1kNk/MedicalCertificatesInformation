using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.ReportViewModels
{
    public class DefaultReportModel
    {
        [Display(Name = "Тип отчета")]
        [Required(ErrorMessage = "Тип отчета должен быть выбран")]
        public int ReportType { get; set; }

        [Required]
        public IReadOnlyList<ReportTypePair> ReportTypes { get; set; }

        [Display(Name = "Тип сортировки")]
        [Required(ErrorMessage = "Тип сортировки должен быть выбран")]
        public int SortBy { get; set; }

        [Required]
        public IReadOnlyList<ReportSortPair> ReportSorts { get; set; }

        [Display(Name = "каких студентов показать?")]
        [Required(ErrorMessage = "Выберите каких студентов нужно показать")]
        public int WhichPeopleChose { get; set; }

        [Required]
        public IReadOnlyList<WhichPeopleChosePair> ReportWhichPeopleChose { get; set; }

        [Display(Name = "Дата")]
        [StringLength(10, ErrorMessage = "Поле'{0}' должно иметь {1} знаков", MinimumLength = 10)]
        public string Date { get; set; }

        [Display(Name = "В течении скольки месяцев?")]
        public int MonthsCount { get; set; }
    }
}
