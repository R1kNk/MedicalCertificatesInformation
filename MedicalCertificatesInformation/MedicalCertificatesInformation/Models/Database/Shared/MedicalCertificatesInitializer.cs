using MedicalCertificatesInformation.Models.Database.Models;
using System;

namespace MedicalCertificatesInformation.Models.Database.Shared
{
    public class MedicalCertificatesInitializer : DropCreateIfEmpty<MedicalCertificatesContext>
    {
        protected override void Seed(MedicalCertificatesContext context)
        {
            
        }
    }
}