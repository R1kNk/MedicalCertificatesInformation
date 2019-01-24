using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MedicalCertificates.Repositories;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.AuthServices;
using System;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.ModelsServices;
using MedicalCertificates.Service.BusinessServices;

namespace MedicalCertificates.Service.ServiceConfigurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection MedicalCertificatesServiceDependencies(this IServiceCollection services)
        {

            services.AddDbContext<MedicalCertificatesDbContext>(options=>options.UseSqlServer(MedicalCertificatesDbContext.connectionString));
      
            services.AddIdentity<ApplicationUser, ApplicationRole>()
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
            services.AddTransient(typeof(IUserService<ApplicationUser>), typeof(UserService));
            services.AddTransient(typeof(IRoleManager<ApplicationRole>), typeof(ApplicationRoleManager));
            services.AddTransient(typeof(GroupOfStudentReportService<Group, IGroupService>), typeof(GroupReportService));
            services.AddTransient(typeof(GroupOfStudentReportService<Department, IDepartmentService>), typeof(DepartmentReportService));
            services.AddTransient(typeof(GroupOfStudentReportService<Course, ICourseService>), typeof(CourseReportService));

            services.AddTransient(typeof(IRoleManager<ApplicationRole>), typeof(ApplicationRoleManager));
            services.AddTransient<IMedicalCertificatesUnitOfWork, MedicalCertificatesUnitOfWork>();

            services.AddTransient<IPhysicalEducationService, PhysicalEducationService>();
            services.AddTransient<IHealthGroupService, HealthGroupService>();
            services.AddTransient<IMedicalCertificateService, MedicalCertificateService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStringConverterService, StringConverterService>();

            return services;

        }
    }
}
