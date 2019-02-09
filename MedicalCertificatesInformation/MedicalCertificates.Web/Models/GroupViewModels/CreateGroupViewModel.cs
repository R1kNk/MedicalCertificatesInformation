using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.GroupViewModels
{
    public class CreateGroupViewModel
    {
        [Required(ErrorMessage = "Поле 'Название группы' должно быть заполнено")]
        [Display(Name = "Название группы")]
        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Название группы не должно содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(5, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 1)]
        public string Name { get; set; }

        public string GoogleDriveFolderId { get; set; }

        [Required(ErrorMessage = "Поле 'Идентификатор курса' должно быть заполнено")]
        public int CourseId { get; set; }
    }
}
