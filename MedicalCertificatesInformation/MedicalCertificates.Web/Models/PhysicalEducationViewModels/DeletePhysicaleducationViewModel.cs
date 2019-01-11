using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.PhysicalEducationViewModels
{
    public class DeletePhysicalEducationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Название группы по физкультуре")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
