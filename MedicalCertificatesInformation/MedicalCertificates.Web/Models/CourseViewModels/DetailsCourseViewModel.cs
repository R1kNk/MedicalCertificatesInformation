using MedicalCertificates.DomainModel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.CourseViewModels
{
    public class DetailsCourseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public virtual Department Department { get; set; }

        [Required]
        public virtual List<Group> Groups { get; set; }
    }
}
