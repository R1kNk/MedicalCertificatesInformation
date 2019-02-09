using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.HealthGroupViewModels
{
    public class DetailsHealthGroupViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Группа здоровья' должно быть заполнено")]
        [Display(Name = "Группа здоровья")]
        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Группа здоровья не должна содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(50, ErrorMessage = "{0} должна иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 1)]
        public string Name { get; set; }

        public List<MedicalCertificate> MedicalCertificates { get; set; }
    }
}
