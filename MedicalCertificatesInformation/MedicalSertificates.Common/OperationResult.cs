using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalSertificates.Common
{
    public class OperationResult<T>
    {
        public bool IsSucceed { get; }
        public IReadOnlyCollection<T> Errors { get; }

        public OperationResult()
        {
            IsSucceed = true;
            Errors = new List<T>();
        }

        private OperationResult(IReadOnlyCollection<T> errors)
        {
            IsSucceed = false;
            Errors = errors;
        }

        public static OperationResult<T> CreateSuccessfulResult()
        {
            return new OperationResult<T>();
        }

        public static OperationResult<T> CreateUnsuccessfulResult(IReadOnlyCollection<T> errors)
        {
            return new OperationResult<T>(errors);
        }
    }
}
