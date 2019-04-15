using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Auth
{
    public interface ISignInManager<TUser> where TUser : IdentityUser
    {
       
        Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure);

        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        Task<TUser> GetTwoFactorAuthenticationUserAsync();

        Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);

        Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);

        Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);

        Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null);

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);

        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);

        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);

        bool IsSignedIn(ClaimsPrincipal principal);

        Task SignOutAsync();
    }
}
