using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.TreeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    public class TreeController : Controller
    {
        private readonly IUserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService<ApplicationUser> _userService;
        private readonly IDepartmentService _departmentService;

        public TreeController(IUserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IUserService<ApplicationUser> userService, IDepartmentService departmentService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _departmentService = departmentService;
        }

        public async Task<JsonResult> GetManagementHierarchy()
        {
            var curUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            IReadOnlyList<Department> departments = new List<Department>();
            if ( await _userManager.IsInRoleAsync(curUser, "Admin"))
            {
                 departments = await _departmentService.GetAllAsync();

            }
            else
            {
                var hierarchy = _userService.GetUserManagementHierarchy(curUser);
                departments = hierarchy.Departments;
            }

            List<DepartmentNode> departmentNodes = new List<DepartmentNode>();
            foreach(var department in departments)
            {
                var departmentNode = MakeDepartmentNode(department);
                foreach(var course in department.Courses)
                {
                    var courseNode = MakeCourseNode(course, department.Id);
                    foreach(var group in course.Groups)
                    {
                        var groupNode = MakeGroupNode(group, course.Id);
                        foreach(var student in group.Students)
                        {
                            var studentNode = MakeStudentNode(student, group.Id);
                            for(int i =0; i< student.MedicalCertificates.Count; i++)
                            {
                                var certificateNode = MakeCertificateNode(student.MedicalCertificates[i], ("Медицинская справка № " + (i + 1)).ToString(), student.Id);
                                studentNode.children.Add(certificateNode);
                            }

                            groupNode.children.Add(studentNode);
                        }
                        courseNode.children.Add(groupNode);
                    }
                    departmentNode.children.Add(courseNode);
                }
                departmentNodes.Add(departmentNode);
            }
            var json = Json(departmentNodes);
            return json;
        }

        private CertificateNode MakeCertificateNode(MedicalCertificate certificate, string title, int parentId)
        {
            CertificateNode node = new CertificateNode() { modelId = certificate.Id, parentId = parentId, title = title };
            return node;
        }

        private StudentNode MakeStudentNode(Student student, int parentId)
        {
            StudentNode node = new StudentNode() { modelId = student.Id, parentId = parentId, title = student.Surname + " "+student.Name };
            return node;
        }

        private GroupNode MakeGroupNode(Group group, int parentId)
        {
            GroupNode node = new GroupNode() { modelId = group.Id, parentId = parentId, title = group.Name };
            return node;
        }

        private CourseNode MakeCourseNode(Course course, int parentId)
        {
            CourseNode node = new CourseNode() { modelId = course.Id, parentId = parentId, title = course.Number + " курс" };
            return node;
        }

        private DepartmentNode MakeDepartmentNode(Department department)
        {
            DepartmentNode node = new DepartmentNode() { modelId = department.Id, title = department.Name };
            return node;
        }
    }
}