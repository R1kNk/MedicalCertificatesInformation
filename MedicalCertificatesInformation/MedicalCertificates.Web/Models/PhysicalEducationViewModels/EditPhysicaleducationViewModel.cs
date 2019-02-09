using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.PhysicalEducationViewModels
{
    public class EditPhysicaleducationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название поликлиники' должно быть заполнено")]
        [Display(Name = "Название группы по физкультуре")]
        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Группа по физкультуре не должна содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(50, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
