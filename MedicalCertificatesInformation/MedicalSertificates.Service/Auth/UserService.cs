using MedicalSertificates.Common;
using MedicalSertificates.Service.Auth.ErrorsFetch;
using MedicalSertificates.Service.Interfaces.Auth;
using MedicalSertificates.Service.Interfaces.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalSertificates.Service.Auth
{
    class UserService<TUser> : IUserService<TUser> where TUser : class
    {

        private readonly IUserManager<TUser> _userManager;

        public UserService(IUserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult<IdentityResultError>> ChangePasswordAsync(TUser user, string currentPassword, string newPassword)
        {
            var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> DeleteAsync(TUser user)
        {
            var identityResult = await _userManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;

        }

        public async Task<TUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<OperationResult<IdentityResultError>> RemoveFromRoleAsync(TUser user, string role)
        {
            var identityResult = await _userManager.RemoveFromRoleAsync(user, role);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> RemoveFromRolesAsync(TUser user, IEnumerable<string> roles)
        {
            var identityResult = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetEmailAsync(TUser user, string email)
        {
            var identityResult = await _userManager.SetEmailAsync(user, email);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetUserNameAsync(TUser user, string userName)
        {
            var identityResult = await _userManager.SetUserNameAsync(user, userName);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> UpdateAsync(TUser user)
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
