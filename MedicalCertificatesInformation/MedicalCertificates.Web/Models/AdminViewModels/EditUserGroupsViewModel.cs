using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.AdminViewModels
{
    public class EditUserGroupsViewModel
    {
        public string UserId { get; set; }
        public List<int> ActiveGroupsId { get; set; }
        public List<int> InactiveGroupId { get; set; }
    }
}
