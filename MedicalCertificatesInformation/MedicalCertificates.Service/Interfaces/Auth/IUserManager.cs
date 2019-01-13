using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Auth
{
    public interface IUserManager<TUser> where TUser : class
    {
        IQueryable<TUser> Users { get; }

        Task<IdentityResult> CreateAsync(TUser user, string password);

        Task<bool> HasPasswordAsync(TUser user);

        Task<IdentityResult> AddPasswordAsync(TUser user, string password);

        Task<IdentityResult> CreateAsync(TUser user);

        Task<bool> IsInRoleAsync(TUser user, string role);

        Task<TUser> FindByIdAsync(string userId);

        Task<TUser> FindByEmailAsync(string email);

        Task<TUser> FindByNameAsync(string userName);

        Task<IList<string>> GetRolesAsync(TUser user);

        Task<TUser> GetUserAsync(ClaimsPrincipal principal);

        string GetUserName(ClaimsPrincipal principal);

        Task<string> GetUserNameAsync(TUser user);

        string GetUserId(ClaimsPrincipal principal);

        Task<string> GetUserIdAsync(TUser user);

        Task<IdentityResult> AddLoginAsync(TUser user, UserLoginInfo login);

        Task<IdentityResult> AddToRoleAsync(TUser user, string role);

        Task<IdentityResult> AddToRolesAsync(TUser user, IEnumerable<string> roles);

        Task<IdentityResult> SetEmailAsync(TUser user, string email);

        Task<IdentityResult> SetUserNameAsync(TUser user, string userName);

        Task<IdentityResult> RemoveFromRoleAsync(TUser user, string role);

        Task<IdentityResult> RemoveFromRolesAsync(TUser user, IEnumerable<string> roles);

        Task<IdentityResult> RemoveLoginAsync(TUser user, string loginProvider, string providerKey);

        Task<IdentityResult> RemovePasswordAsync(TUser user);

        Task<IdentityResult> DeleteAsync(TUser user);

        Task<IdentityResult> ConfirmEmailAsync(TUser user, string token);

        Task<bool> IsEmailConfirmedAsync(TUser user);

        Task<string> GeneratePasswordResetTokenAsync(TUser user);

        Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword);

        Task<IdentityResult> ChangeEmailAsync(TUser user, string newEmail, string token);

        Task<IdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword);

        Task<IdentityResult> UpdateAsync(TUser user);
    }
}
