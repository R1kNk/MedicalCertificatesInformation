using MedicalCertificates.Common;
using MedicalCertificates.Service.Auth.ErrorsFetch;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Models
{
    interface IUserService<TUser> where TUser : class
    {
        Task<IList<string>> GetRolesAsync(TUser user);
        Task<TUser> GetUserAsync(ClaimsPrincipal principal);
        Task<OperationResult<IdentityResultError>> RemoveFromRoleAsync(TUser user, string role);
        Task<OperationResult<IdentityResultError>> RemoveFromRolesAsync(TUser user, IEnumerable<string> roles);
        Task<OperationResult<IdentityResultError>> ChangePasswordAsync(TUser user, string currentPassword, string newPassword);
        Task<OperationResult<IdentityResultError>> SetUserNameAsync(TUser user, string userName);
        Task<OperationResult<IdentityResultError>> SetEmailAsync(TUser user, string email);
        Task<OperationResult<IdentityResultError>> DeleteAsync(TUser user);
        Task<OperationResult<IdentityResultError>> UpdateAsync(TUser user);
    }
}
