using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.MedicalCertificatesViewModels
{
    public class EditMedicalCertificatesViewModel : GeneralMedicalCertificatesInfo
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Дата начала' должно быть заполнено")]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Поле 'Дата окончания' должно быть заполнено")]
        [Display(Name = "Дата окончания")]
        public DateTime FinishDate { get; set; }

        [Required(ErrorMessage = "Поле 'Длительность действия' должно быть заполнено")]
        [Display(Name = "Длительность действия")]
        public TimeSpan CertificateTerm { get; set; }
    }
}
