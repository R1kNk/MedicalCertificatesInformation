using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.DepartmentViewModels
{
    public class DeleteDepartmentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Название отделения")]
        [StringLength(60, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
