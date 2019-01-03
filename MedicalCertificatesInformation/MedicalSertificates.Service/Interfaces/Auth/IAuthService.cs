using MedicalSertificates.Common;
using MedicalSertificates.DomainModel.Models;
using MedicalSertificates.Service.Auth.ErrorsFetch;
using System.Threading.Tasks;

namespace MedicalSertificates.Service.Interfaces.Auth
{
    interface IAuthService
    {
        Task<OperationResult<IdentityResultError>> RegisterUserAsync(ApplicationUser user);
        Task<OperationResult<SignInResultError>> LoginUserAsync(ApplicationUser user, bool rememberMe, bool lockout);
        Task LogoutUserAsync(ApplicationUser user);
    }
}
