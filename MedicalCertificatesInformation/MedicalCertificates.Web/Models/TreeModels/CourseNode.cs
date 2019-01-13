using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.TreeModels
{
    public class CourseNode
    {
        public string title { get; set; }

        public int modelId { get; set; }

        public int parentId { get; set; }

        public string type = "course";

        public bool folder = true;

        public List<GroupNode> children = new List<GroupNode>();

    }
}
