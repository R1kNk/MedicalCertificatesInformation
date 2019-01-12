using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.StudentViewModels
{
    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "Поле 'Имя' должно быть заполнено")]
        [Display(Name = "Имя")]
        [StringLength(30, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Фамилия' должно быть заполнено")]
        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Surname { get; set; }

        public string GoogleDriveFolderId { get; set; }

        [Required(ErrorMessage = "Поле 'Идентификатор группы' должно быть заполнено")]
        public int GroupId { get; set; }
    }
}
