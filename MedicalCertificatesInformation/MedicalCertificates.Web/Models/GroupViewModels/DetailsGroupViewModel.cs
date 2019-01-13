using MedicalCertificates.DomainModel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.GroupViewModels
{
    public class DetailsGroupViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Название группы")]
        [StringLength(5, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 1)]
        public string Name { get; set; }

        public string GoogleDriveFolderId { get; set; }

        [Required]
        public virtual Course Course { get; set; }

        [Required]
        public virtual List<Student> Students { get; set; }
    }
}
