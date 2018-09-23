using MedicalCertificatesInformation.Models.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalCertificatesInformation.Models.Database.Shared
{
    public class MedicalCertificatesInitializer : DropCreateIfEmpty<MedicalCertificatesContext>
    {
        protected override void Seed(MedicalCertificatesContext context)
        {
            
        }
    }
}