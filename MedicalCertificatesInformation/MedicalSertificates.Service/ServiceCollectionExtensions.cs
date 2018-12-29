using MedicalSertificates.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MedicalSertificates.DomainModel.Models;
using Microsoft.AspNetCore.Identity;

namespace MedicalSertificates.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServiceDependencies(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<MedicalSertificatesDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MedicalSertificatesDbContext>()
                .AddDefaultTokenProviders();
            return services;

        }
    }
}
