using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.MedicalCertificatesViewModels
{
    public class DetailsMedicalCertificatesViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Дата начала' должно быть заполнено")]
        [StringLength(10, ErrorMessage = "'{0}' должно иметь {1} знаков", MinimumLength = 10)]
        [Display(Name = "Дата начала")]
        public string StartDate { get; set; }

        //[Required(ErrorMessage = "Поле 'Дата окончания' должно быть заполнено")]
        [Display(Name = "Дата окончания")]
        [StringLength(10, ErrorMessage = "'{0}' должно иметь {1} знаков", MinimumLength = 10)]
        public string FinishDate { get; set; }

        [Required(ErrorMessage = "Поле 'Длительность действия' должно быть заполнено")]
        [Display(Name = "Длительность действия")]
        public int CertificateTerm { get; set; }

        public string GoogleDriveImageId { get; set; }

        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public virtual HealthGroup HealthGroup { get; set; }

        [Required]
        public virtual PhysicalEducation PhysicalEducation { get; set; }
    }
}
