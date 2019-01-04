using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MedicalCertificates.Repositories;
using MedicalCertificates.DomainModel.Models;

namespace MedicalCertificates.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServiceDependencies(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<MedicalCertificatesDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MedicalCertificatesDbContext>()
                //.AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
            return services;

        }
    }
}
