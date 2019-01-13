using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.Web.Models.HospitalViewModels
{
    public class CreateHospitalViewModel
    {
        [Required(ErrorMessage = "Поле 'Поликлиника' должно быть заполнено")]
        [Display(Name = "Поликлиники")]
        [StringLength(50, ErrorMessage = "{0} должна иметь хотя бы {2} и максимально {1} знаков.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Номер телефона поликлиники' должно быть заполнено")]
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Номер телефона должен иметь формат +375/80XXXXXXXXX")]
        [Display(Name = "Номер телефона поликлиники")]
        public string TelephoneNumber { get; set; }
    }
}
