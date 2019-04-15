using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Service.ReportModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCertificates.Service.AuthServices
{
    class UserService: IUserService<ApplicationUser>
    {

        private readonly IUserManager<ApplicationUser> _userManager;
        private readonly IStringConverterService _converter;

        public UserService(IUserManager<ApplicationUser> userManager, IStringConverterService converter)
        {
            _userManager = userManager;
            _converter = converter;
        }

        public async Task<OperationResult<IdentityResultError>> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> DeleteAsync(ApplicationUser user)
        {
            var identityResult = await _userManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;

        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<OperationResult<IdentityResultError>> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            var identityResult = await _userManager.RemoveFromRoleAsync(user, role);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var identityResult = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetEmailAsync(ApplicationUser user, string email)
        {
            var identityResult = await _userManager.SetEmailAsync(user, email);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetUserNameAsync(ApplicationUser user, string userName)
        {
            var identityResult = await _userManager.SetUserNameAsync(user, userName);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> SetPseudonimAsync(string userId, string pseudonim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(new List<IdentityResultError>() { IdentityResultError.UserNotFound });

            //user.Pseudonim = pseudonim;
            var identityResult = await _userManager.SetUserNameAsync(user, _converter.ConvertToUsername(_converter.ConvertFromRussianToEnglish(pseudonim)));
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            user.Pseudonim = pseudonim;

            identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<IdentityResultError>> UpdateAsync(ApplicationUser user)
        {
            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {

                var errors = ErrorFetcher.FetchIdentityErrors(identityResult);

                return OperationResult<IdentityResultError>.CreateUnsuccessfulResult(errors);
            }

            return OperationResult<IdentityResultError>.CreateSuccessfulResult();
        }

        public UserManagementHierarchy GetUserManagementHierarchy(ApplicationUser user)
        {
            if (user == null)
                return null;
            if(user is DefaultUser)
            {
                var defaultUser = user as DefaultUser;
                List<Department> departmentsResult = new List<Department>();
                List<Group> groups = defaultUser.Groups;
                foreach (var group in groups)
                {
                    var department = group.Course.Department;
                    Department findedDep = departmentsResult.Where(p => p.Id == department.Id).SingleOrDefault();
                    if (findedDep == null)
                    {
                        findedDep = new Department()
                        {
                            Id = department.Id,
                            Name = department.Name,
                            Courses = new List<Course>()
                        };
                        departmentsResult.Add(findedDep);
                    }
                    Course findedCourse = findedDep.Courses.Where(p => p.Id == group.CourseId).SingleOrDefault();
                    if (findedCourse == null)
                    {
                        findedCourse = new Course()
                        {
                            Id = group.CourseId,
                            Number = group.Course.Number,
                            Department = findedDep,
                            DepartmentId = findedDep.Id,
                            Groups = new List<Group>()
                        };
                        findedDep.Courses.Add(findedCourse);
                    }
                    Group findedGroup = findedCourse.Groups.Where(p => p.Id == group.Id).SingleOrDefault();
                    if (findedGroup == null)
                    {
                        findedGroup = new Group()
                        {
                            Id = group.Id,
                            Name = group.Name,
                            GoogleDriveFolderId = "",
                            CourseId = findedCourse.Id,
                            Course = findedCourse,
                            Students = new List<Student>()
                        };

                        foreach (var student in group.Students)
                        {
                            Student newStudent = new Student()
                            {
                                Id = student.Id,
                                Name = student.Name,
                                Surname = student.Surname,
                                GoogleDriveFolderId = "",
                                GroupId = findedGroup.Id,
                                Group = findedGroup,
                                MedicalCertificates = new List<MedicalCertificate>()
                            };

                            foreach (var certificate in student.MedicalCertificates)
                            {
                                MedicalCertificate newMedicalCertificate = new MedicalCertificate()
                                {
                                    Id = certificate.Id,
                                    StudentId = newStudent.Id,
                                    Student = newStudent
                                };
                                newStudent.MedicalCertificates.Add(newMedicalCertificate);
                            }
                            findedGroup.Students.Add(newStudent);
                        }
                        findedCourse.Groups.Add(findedGroup);
                    }
                }
                return new UserManagementHierarchy(departmentsResult);
            }
            else if(user is DepartmentManagerUser)
            {
                var departmentManagerUser = user as DepartmentManagerUser;
                var departmentsList = new List<Department>();
                if (departmentManagerUser.Department != null)
                {
                    departmentsList.Add(departmentManagerUser.Department);
                }
                return new UserManagementHierarchy(departmentsList);
            }
            else
            {
                return new UserManagementHierarchy(new List<Department>());
            }
            
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsersSortedAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            users = users.OrderBy(p => p.Pseudonim).ToList();
            return users;
        }
    }
}
