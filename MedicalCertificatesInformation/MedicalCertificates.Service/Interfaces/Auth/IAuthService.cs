using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Auth
{
    interface IAuthService
    {
        Task<OperationResult<IdentityResultError>> RegisterUserAsync(ApplicationUser user);
        Task<OperationResult<SignInResultError>> LoginUserAsync(ApplicationUser user, bool rememberMe, bool lockout);
        Task LogoutUserAsync(ApplicationUser user);
    }
}
