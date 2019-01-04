namespace MedicalCertificates.Service.ErrorsFetch
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
