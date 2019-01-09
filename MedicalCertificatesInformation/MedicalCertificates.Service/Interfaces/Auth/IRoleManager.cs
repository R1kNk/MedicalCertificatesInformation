using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.Interfaces.Auth
{
    public interface IRoleManager<TRole> where TRole : class
    {
        IQueryable<TRole> Roles { get; }

        Task<IdentityResult> CreateAsync(TRole role);

        Task<IdentityResult> DeleteAsync(TRole role);

        Task<TRole> FindByIdAsync(string roleId);

        Task<TRole> FindByNameAsync(string roleName);

        Task<string> GetRoleIdAsync(TRole role);

        Task<string> GetRoleNameAsync(TRole role);

        Task<bool> RoleExistsAsync(string roleName);

        Task<IdentityResult> SetRoleNameAsync(TRole role, string name);

        Task<IdentityResult> UpdateAsync(TRole role);
    }
}
