using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MedicalCertificates.Repositories;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.AuthServices;
using System;

namespace MedicalCertificates.Service.ServiceConfigurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection MedicalCertificatesServiceDependencies(this IServiceCollection services)
        {

            services.AddDbContext<MedicalCertificatesDbContext>(options=>options.UseSqlServer(MedicalCertificatesDbContext.connectionString));
      
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MedicalCertificatesDbContext>()
                .AddErrorDescriber<RussianIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromSeconds(10));
            services.AddAuthentication()
                .Services.ConfigureApplicationCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient(typeof(ISignInManager<ApplicationUser>), typeof(ApplicationSignInManager));
            services.AddTransient(typeof(IUserManager<ApplicationUser>), typeof(ApplicationUserManager));


            return services;

        }
    }
}
