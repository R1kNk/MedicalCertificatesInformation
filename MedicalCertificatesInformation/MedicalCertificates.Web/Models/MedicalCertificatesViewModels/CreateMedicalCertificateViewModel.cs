using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.MedicalCertificatesViewModels
{
    public class CreateMedicalCertificateViewModel : GeneralMedicalCertificatesInfo
    {
        [Required]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Поле 'Дата начала' должно быть заполнено")]
        [StringLength(10, ErrorMessage = "'{0}' должно иметь {1} знаков", MinimumLength = 10)]
        [Display(Name = "Дата начала")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Поле 'Дата окончания' должно быть заполнено")]
        [Display(Name = "Дата окончания")]
        [StringLength(10, ErrorMessage = "Поле '{0}; должно иметь {1} знаков", MinimumLength = 10)]
        public string FinishDate { get; set; }

        [Required(ErrorMessage = "Поле 'Длительность действия' должно быть заполнено")]
        [Display(Name = "Длительность действия")]
        public int CertificateTerm { get; set; }

        [Required]
        public bool IsUsingTerm { get; set; }

    }
}
