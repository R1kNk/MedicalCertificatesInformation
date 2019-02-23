using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.StudentViewModels
{
    public class DetailsStudentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' должно быть заполнено")]
        [Display(Name = "Имя")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Фамилия' должно быть заполнено")]
        [Display(Name = "Фамилия")]
        [StringLength(60, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 2)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле 'Дата рождения' должно быть заполнено")]
        [Display(Name = "Дата рождения")]
        [StringLength(10, ErrorMessage = "Поле '{0}; должно иметь {1} знаков", MinimumLength = 10)]
        public string BirthDate { get; set; }

        [Display(Name = "Отчество")]
        public string SecondName { get; set; }

        public string GoogleDriveFolderId { get; set; }

        [Required]
        public Group Group { get; set; }

        [Required]
        public List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}
