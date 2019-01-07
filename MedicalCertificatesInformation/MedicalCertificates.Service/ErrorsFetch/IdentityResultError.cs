namespace MedicalCertificates.Service.ErrorsFetch
{
    public enum IdentityResultError
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
