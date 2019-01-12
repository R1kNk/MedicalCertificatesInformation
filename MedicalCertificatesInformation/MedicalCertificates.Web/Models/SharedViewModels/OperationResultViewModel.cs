using MedicalCertificates.Web.Models.SharedEntities;

namespace MedicalCertificates.Web.Models.SharedViewModels
{
    public class OperationResultViewModel
    {
        public bool IsSuccessfully { get; set; }

        public OperationResultEnum OperationResult { get; set; } 

        public string AdditionalMessage { get; set; }

        public OperationResultViewModel(bool isSuccess, OperationResultEnum operationResult, string additionalMessage = "")
        {
            AdditionalMessage = additionalMessage;
            IsSuccessfully = isSuccess;
            OperationResult = operationResult;
        }
    }
}
