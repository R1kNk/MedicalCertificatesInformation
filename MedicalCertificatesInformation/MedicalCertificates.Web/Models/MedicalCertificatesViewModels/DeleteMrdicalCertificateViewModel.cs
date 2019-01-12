using MedicalCertificates.DomainModel.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.MedicalCertificatesViewModels
{
    public class DeleteMedicalCertificateViewModel
    {
        [Required]
        public int Id { get; set; }

        public virtual Student Student { get; set; }

        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата окончания")]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Длительность действия")]
        public TimeSpan CertificateTerm { get; set; }
    }
}
