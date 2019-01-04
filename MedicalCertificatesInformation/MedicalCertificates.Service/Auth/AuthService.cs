using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Auth.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Auth;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Auth
{
    class AuthService : IAuthService
    {
        private readonly IUserManager<ApplicationUser> _userManager;
        private readonly ISignInManager<ApplicationUser> _signInManager;

        public AuthService(IUserManager<ApplicationUser> userManager, ISignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<OperationResult<IdentityResultError>> RegisterUserAsync(ApplicationUser user)
        {
            var identityResult = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!identityResult.Succeeded)
            {
                
                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<SignInResultError>> LoginUserAsync(ApplicationUser user, bool rememberMe = false, bool lockout = false)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, rememberMe, lockout);
            if (!signInResult.Succeeded)
            {
                var errors =  ErrorFetcher.FetchLogInErrors(signInResult);

                return OperationResult<SignInResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<SignInResultError>.CreateSuccessfulResult();
        }

        public async Task LogoutUserAsync(ApplicationUser user)
        {
            await _signInManager.SignOutAsync();
        }

    }
}
