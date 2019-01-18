using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.AdminViewModels
{
    public class EditUserNameViewModel
    {
        [Required(ErrorMessage ="Поле 'Пользователь' должно быть заполнено")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Поле 'Новое имя пользователя' должно быть заполнено")]
        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Новое имя пользователя не должно содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(40, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 6)]
        [Display(Name = "Новое имя пользователя")]
        public string Username { get; set; }
    }
}
