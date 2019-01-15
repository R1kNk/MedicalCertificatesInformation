using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.TreeModels
{
    public class GroupNode
    {
        public string title { get; set; }

        public int modelId { get; set; }

        public int parentId { get; set; }

        public string userRole { get; set; }

        public string type = "group";

        public bool folder = true;

        public List<StudentNode> children = new List<StudentNode>();

    }
}
