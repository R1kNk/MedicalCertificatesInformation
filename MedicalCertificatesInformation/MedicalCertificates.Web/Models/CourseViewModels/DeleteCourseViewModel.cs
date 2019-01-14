using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.CourseViewModels
{
    public class DeleteCourseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Номер курса' должно быть заполнено")]
        [Display(Name = "Номер курса")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Отделения")]
        public string DepartmentName { get; set; }
    }
}
