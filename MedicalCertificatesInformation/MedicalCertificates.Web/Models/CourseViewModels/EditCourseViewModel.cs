using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.CourseViewModels
{
    public class EditCourseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Номер курса' должно быть заполнено")]
        [Display(Name = "Номер курса")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Название отделения")]
        public string DepartmentName { get; set; }

        public IReadOnlyList<int> CourseNumbers { get; set; }

    }
}
