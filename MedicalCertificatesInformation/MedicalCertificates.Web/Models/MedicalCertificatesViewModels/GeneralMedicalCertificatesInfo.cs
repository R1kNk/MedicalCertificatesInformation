using MedicalCertificates.DomainModel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.MedicalCertificatesViewModels
{
    public class GeneralMedicalCertificatesInfo
    {

        [Required(ErrorMessage = "Группа здоровья должна быть выбрана")]
        [Display(Name = "Группа здоровья")]
        public int HealthGroupId { get; set; }

        public IReadOnlyList<HealthGroup> HealthGroups { get; set; }

        [Required(ErrorMessage = "Группа по физкультуре должна быть выбрана")]
        [Display(Name = "Группа по физкультуре")]
        public int PhysicalEducationId { get; set; }

        public virtual IReadOnlyList<PhysicalEducation> PhysicalEducations { get; set; }

        [Required(ErrorMessage = "Поликлиника должна быть выбрана")]
        [Display(Name = "Поликлиника которая выдала справку")]
        public int HospitalId { get; set; }

        public virtual IReadOnlyList<Hospital> Hospitals { get; set; }

        public virtual List<CertificateTerm> Terms { get; set; }
    }
}
