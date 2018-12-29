using MedicalCertificatesInformation.Models.Database.Models;
using System;
using System.Collections.Generic;

namespace MedicalCertificatesInformation.Models.Database.Shared
{
    public class MedicalCertificatesInitializer : DropCreateIfEmpty<MedicalCertificatesContext>
    {
        protected override void Seed(MedicalCertificatesContext context)
        {
            List<Department> departments = new List<Department>() {
                new Department() { Name = "Банковское дело" },
                new Department() { Name = "Бухгалтерский учет, анализ и контроль" },
                new Department() { Name="Коммерческая деятельность"},
                new Department() { Name="Правоведение"},
                new Department(){ Name="Программное обеспечение информационных технологий"},
                new Department(){ Name="Экономика и организация производства"}
            };
        }
    }
}