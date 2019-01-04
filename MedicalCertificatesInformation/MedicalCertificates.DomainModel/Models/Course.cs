using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Number { get; set; }
   
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual List<Group> Groups { get; set; }
    }
}