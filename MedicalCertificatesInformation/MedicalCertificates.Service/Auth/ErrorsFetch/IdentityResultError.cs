namespace MedicalCertificates.Service.Auth.ErrorsFetch
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
