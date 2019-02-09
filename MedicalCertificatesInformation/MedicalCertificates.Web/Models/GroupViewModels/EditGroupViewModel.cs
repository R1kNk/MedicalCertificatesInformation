using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.GroupViewModels
{
    public class EditGroupViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Название группы")]
        [Required(ErrorMessage ="Название группы должно быть заполнено")]
        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Название группы не должно содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(5, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}
