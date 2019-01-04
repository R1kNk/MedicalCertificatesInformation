using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ErrorsFetch
{
    class ErrorFetcher
    {
        public static IReadOnlyCollection<IdentityResultError> FetchIdentityErrors(IdentityResult identityResult)
        {
            var errors = new List<IdentityResultError>();
            var errorTypeNames = Enum.GetNames(typeof(IdentityResultError));

            foreach (var receivedIdentityResultError in identityResult.Errors)
            {
                foreach (var errorTypeName in errorTypeNames)
                {
                    if (receivedIdentityResultError.Code == errorTypeName)
                        errors.Add((IdentityResultError)Enum.Parse(typeof(IdentityResultError), errorTypeName, true));
                }

            }
            return errors;
        }

        public static IReadOnlyCollection<SignInResultError> FetchLogInErrors(SignInResult signInResult)
        {
            var errors = new List<SignInResultError>();
            var errorTypeNames = Enum.GetNames(typeof(SignInResultError));
   
            foreach (var errorTypeName in errorTypeNames)
            {
              if (signInResult.IsLockedOut)
                errors.Add((SignInResultError)Enum.Parse(typeof(IdentityResultError), "IsLockedOut", true));

              else if(signInResult.IsNotAllowed)
                  errors.Add((SignInResultError)Enum.Parse(typeof(IdentityResultError), "IsNotAllowed", true));

              else if (signInResult.RequiresTwoFactor)
                  errors.Add((SignInResultError)Enum.Parse(typeof(IdentityResultError), "RequiresTwoFactor", true));
            }
            return errors;
        }

    }
}
