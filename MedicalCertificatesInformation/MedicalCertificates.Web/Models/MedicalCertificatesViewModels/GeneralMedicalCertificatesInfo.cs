using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.MedicalCertificatesViewModels
{
    public class GeneralMedicalCertificatesInfo
    {

        [Required]
        [Display(Name = "Группа здоровья")]
        public int HealthGroupId { get; set; }

        public IReadOnlyList<HealthGroup> HealthGroups { get; set; }

        [Required]
        [Display(Name = "Группа по физкультуре")]
        public int PhysicalEducationId { get; set; }

        public virtual IReadOnlyList<PhysicalEducation> PhysicalEducations { get; set; }

        [Required]
        [Display(Name = "Поликлиника которая выдала справку")]
        public int HospitalId { get; set; }

        public virtual IReadOnlyList<Hospital> Hospitals { get; set; }
    }
}
