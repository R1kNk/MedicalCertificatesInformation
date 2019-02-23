using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.TreeModels;

namespace MedicalCertificates.Web.Models.SharedViewModels
{
    public class OperationResultViewModel
    {
        public bool IsSuccessfully { get; set; }

        public OperationResultEnum OperationResult { get; set; } 

        public string AdditionalMessage { get; set; }

        public GenericNode Node { get; set; }

        public OperationResultViewModel(bool isSuccess, OperationResultEnum operationResult, string additionalMessage = "", GenericNode node = null)
        {
            AdditionalMessage = additionalMessage;
            IsSuccessfully = isSuccess;
            OperationResult = operationResult;
            if (node == null)
            {
                Node = new GenericNode();
            }
            else Node = node;
        }
    }
}
