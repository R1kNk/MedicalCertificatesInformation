using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalSertificates.Service.Auth.ErrorsFetch
{
    enum IdentityResultError
    {
        DefaultError,
        PasswordMismatch,
        LoginAlreadyAssociated,
        InvalidUserName,
        InvalidEmail,
        DuplicateEmail,
        DuplicateUserName,
        PasswordTooShort,
        PasswordRequiresNonAlphanumeric,
        PasswordRequiresDigit,
        PasswordRequiresLower,
        PasswordRequiresUpper
    }
}
