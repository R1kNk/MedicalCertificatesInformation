using MedicalSertificates.Common;
using MedicalSertificates.DomainModel.Models;
using MedicalSertificates.Service.Auth.ErrorsFetch;
using MedicalSertificates.Service.Interfaces.Auth;
using MedicalSertificates.Service.Interfaces.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalSertificates.Service.Auth
{
    class UserService: IUserService<ApplicationUser>
    {

        private readonly IUserManager<ApplicationUser> _userManager;

        public UserService(IUserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult<IdentityResultError>> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> DeleteAsync(ApplicationUser user)
        {
            var identityResult = await _userManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;

        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<OperationResult<IdentityResultError>> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            var identityResult = await _userManager.RemoveFromRoleAsync(user, role);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var identityResult = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetEmailAsync(ApplicationUser user, string email)
        {
            var identityResult = await _userManager.SetEmailAsync(user, email);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetUserNameAsync(ApplicationUser user, string userName)
        {
            var identityResult = await _userManager.SetUserNameAsync(user, userName);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> UpdateAsync(ApplicationUser user)
        {
            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }
    }
}
