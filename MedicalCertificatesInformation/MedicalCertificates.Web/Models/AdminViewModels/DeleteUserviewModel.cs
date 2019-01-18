using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.AdminViewModels
{
    public class DeleteUserViewModel
    {
        [Required(ErrorMessage = "Поле 'Пользователь' должно быть заполнено")]
        public string Id { get; set; }

        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Имя пользователя не должно содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(40, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 6)]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
    }
}
