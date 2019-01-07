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
        public static IServiceCollection MedicalCertificatesServiceDependencies(this IServiceCollection services)
        {

            services.AddDbContext<MedicalCertificatesDbContext>(options=>options.UseSqlServer(MedicalCertificatesDbContext.connectionString));
      
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MedicalCertificatesDbContext>()
                .AddDefaultTokenProviders();
            return services;

        }
    }
}
