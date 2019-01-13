using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.StudentViewModels
{
    public class EditStudentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' должно быть заполнено")]
        [Display(Name = "Имя")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Фамилия' должно быть заполнено")]
        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 2)]
        public string Surname { get; set; }
    }
}
