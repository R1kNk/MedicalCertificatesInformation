using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.StudentViewModels
{
    public class MoveStudentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [StringLength(30, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Surname { get; set; }

        [Required(ErrorMessage ="Выберите группу для перемещения студента")]
        public int GroupId { get; set; }
    }
}
