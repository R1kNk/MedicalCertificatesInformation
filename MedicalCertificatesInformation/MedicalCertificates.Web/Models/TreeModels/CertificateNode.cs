using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Models.TreeModels
{
    public class CertificateNode
    {
        public string title { get; set; }

        public int modelId { get; set; }

        public int parentId { get; set; }

        public string type = "certificate";

        public string userRole { get; set; }
    }
}
