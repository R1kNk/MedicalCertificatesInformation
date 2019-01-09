using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.Interfaces.Common
{
    public interface IStringConverterService
    {
        string ConvertFromRussianToEnglish(string russian);

        string ConvertFromEnglishToRussian(string english);

        string ConvertToUsername(string invalidUsername);
    }
}
