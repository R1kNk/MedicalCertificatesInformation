using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.AdminViewModels
{
    public class EditManagerUserDepartmentViewModel
    {
        public string UserId { get; set; }
        public List<int> DepartmentsId { get; set; }
    }
}
