using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Course> Courses { get; set; }

        public virtual string DepartmentManagerId { get; set; }
        public virtual DepartmentManagerUser DepartmentManager { get; set; }


    }
}