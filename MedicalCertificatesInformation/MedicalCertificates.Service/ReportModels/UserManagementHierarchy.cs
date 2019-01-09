using MedicalCertificates.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ReportModels
{
    public class UserManagementHierarchy
    {
       public IReadOnlyList<Department> Departments { get; private set; }

       public UserManagementHierarchy(IReadOnlyList<Department> departments)
        {
            Departments = departments;
        }
    }
}
