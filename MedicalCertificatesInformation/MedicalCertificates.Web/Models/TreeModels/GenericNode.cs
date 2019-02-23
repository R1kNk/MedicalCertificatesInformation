using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.TreeModels
{
    public class GenericNode
    {
        public string title { get; set; }

        public int modelId { get; set; }

        public bool isHaveParent { get; set; }

        public int parentId { get; set; }

        public string userRole { get; set; }

        public string type { get; set; }

        public bool folder { get; set; }

        public GenericNode(string title, int modelId, bool isHaveParent, string userRole, string type, bool folder, int parentId = 0)
        {
            this.title = title;
            this.modelId = modelId;
            this.isHaveParent = isHaveParent;
            this.userRole = userRole;
            this.type = type;
            this.folder = folder;
            this.parentId = parentId;
        }

        public GenericNode()
        {

        }
    }
}
