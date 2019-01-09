using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage ="Поле 'Имя пользователя' должно быть заполнено")]
        [RegularExpression(@"^[^\\/:?""<>|!]+$", ErrorMessage = "Имя пользователя не должно содержать следующие символы: ^ \\ / : *! ? \" < > |")]
        [StringLength(40, ErrorMessage = "{0} должно иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 6)]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Поля пароля и подтвержденного пароля не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
