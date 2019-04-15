using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.ServiceConfigurations
{
    public class MedicalCertificatesDBConfiguration
    {
        private readonly MedicalCertificatesDbContext _context;
        private readonly IUserManager<ApplicationUser> _userManager;
        private readonly IRoleManager<ApplicationRole> _roleManager;
        private readonly IStringConverterService _converter;

        public MedicalCertificatesDBConfiguration(MedicalCertificatesDbContext context, IUserManager<ApplicationUser> userManager, IRoleManager<ApplicationRole> roleManager, IStringConverterService converter)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _converter = converter;
        }


        public async Task Seed()
        {
            _context.Database.Migrate();

            if (!_context.PhysicalEducations.Any())
            {
                await _context.PhysicalEducations.AddRangeAsync(GetDefaultPhysicalEducations());
                await _context.SaveChangesAsync();
            }

            if (!_context.HealthGroups.Any())
            {
                await _context.HealthGroups.AddRangeAsync(GetDefaultHealthGroups());
                await _context.SaveChangesAsync();
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new ApplicationRole() { Name = "Admin" };
                await _roleManager.CreateAsync(role);

                var user = new ApplicationUser() { UserName = _converter.ConvertToUsername(_converter.ConvertFromRussianToEnglish("Администратор")), Pseudonim = "Администратор", Email = "office@kbp.by" };
                string userPassword = "Adminpassword_1";
                var result = await _userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var role = new ApplicationRole() { Name = "User" };
                await _roleManager.CreateAsync(role);
            }


        }

        List<PhysicalEducation> GetDefaultPhysicalEducations()
        {
            return new List<PhysicalEducation>()
            {
                new PhysicalEducation() { Name = "Основная" },
                new PhysicalEducation() { Name = "Подготовительная" },
                new PhysicalEducation() { Name = "ЛФК" },
                new PhysicalEducation() { Name = "СМГ" }
            };
        }

        List<HealthGroup> GetDefaultHealthGroups()
        {
            return new List<HealthGroup>()
            {
                new HealthGroup() { Name = "1-я группа" },
                new HealthGroup() { Name = "2-я группа" },
                new HealthGroup() { Name = "3-я группа" },
                new HealthGroup() { Name = "4-я группа" },
                new HealthGroup() { Name = "5-я группа" },
            };
        }
    


    }
}
