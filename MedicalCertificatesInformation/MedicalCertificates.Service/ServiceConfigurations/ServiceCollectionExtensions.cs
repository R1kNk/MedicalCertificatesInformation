using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MedicalCertificates.Repositories;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.AuthServices;

namespace MedicalCertificates.Service.ServiceConfigurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection MedicalCertificatesServiceDependencies(this IServiceCollection services)
        {

            services.AddDbContext<MedicalCertificatesDbContext>(options=>options.UseSqlServer(MedicalCertificatesDbContext.connectionString));
      
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MedicalCertificatesDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient(typeof(ISignInManager<ApplicationUser>), typeof(ApplicationSignInManager));
            services.AddTransient(typeof(IUserManager<ApplicationUser>), typeof(ApplicationUserManager));


            return services;

        }
    }
}
