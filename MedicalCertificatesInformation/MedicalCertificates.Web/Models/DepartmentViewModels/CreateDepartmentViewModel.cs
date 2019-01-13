using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.DepartmentViewModels
{
    public class CreateDepartmentViewModel
    {
        [Required(ErrorMessage = "Поле 'Название отделения' должно быть заполнено")]
        [Display(Name = "Название отделения")]
        [StringLength(60, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
