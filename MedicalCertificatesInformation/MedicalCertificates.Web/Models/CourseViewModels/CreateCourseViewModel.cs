using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.CourseViewModels
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = "Поле 'Номер курса' должно быть заполнено")]
        [Display(Name = "Номер курса")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Поле 'Идентификатор отделения' должно быть заполнено")]
        public int DepartmentId { get; set; }

        public IReadOnlyList<int> CourseNumbers { get; set; }
    }
}
