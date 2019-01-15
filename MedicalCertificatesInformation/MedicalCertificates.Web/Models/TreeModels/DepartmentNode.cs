using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.TreeModels
{
    public class DepartmentNode
    {
        public string title { get; set; }

        public int modelId { get; set; }

        public string userRole { get; set; }

        public string type = "department";

        public bool folder = true;

        public List<CourseNode> children = new List<CourseNode>();
    }
}
