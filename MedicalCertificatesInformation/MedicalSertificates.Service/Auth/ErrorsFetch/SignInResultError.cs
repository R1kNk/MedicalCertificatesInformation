using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalSertificates.Service.Auth.ErrorsFetch
{
    enum SignInResultError
    {
        IsLockedOut,
        IsNotAllowed,
        RequiresTwoFactor
    }
}
