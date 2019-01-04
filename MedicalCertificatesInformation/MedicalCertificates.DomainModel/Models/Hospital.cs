using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCertificates.DomainModel.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$")]
        public string TelephoneNumber { get; set; }

        public virtual List<MedicalCertificate> MedicalSertificates { get; set; }
    }
}