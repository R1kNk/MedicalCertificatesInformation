using System.Collections.Generic;

namespace MedicalSertificates.DomainModel.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Number { get; set; }
   
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Group> Groups { get; set; }
    }
}