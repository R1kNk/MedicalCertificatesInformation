using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.HealthGroupViewModels
{
    public class DeleteHealthGroupViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Группа здоровья")]
        [StringLength(50, ErrorMessage = "{0} должна иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}
