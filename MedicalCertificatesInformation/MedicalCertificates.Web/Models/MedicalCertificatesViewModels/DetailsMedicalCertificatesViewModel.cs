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

        [Required]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Дата окончания")]
        public DateTime FinishDate { get; set; }
        [Required]
        [Display(Name = "Длительность действия")]
        public TimeSpan CertificateTerm { get; set; }

        public string GoogleDriveImageId { get; set; }

        [Required]
        public virtual Student Student { get; set; }

        [Required]
        public virtual HealthGroup HealthGroup { get; set; }

        [Required]
        public virtual PhysicalEducation PhysicalEducation { get; set; }

        [Required]
        public virtual Hospital Hospital { get; set; }
    }
}
