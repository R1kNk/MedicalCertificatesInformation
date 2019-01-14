using System;

namespace MedicalCertificates.Web.Models.SharedViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string MessageDescription { get; set; }
    }
}