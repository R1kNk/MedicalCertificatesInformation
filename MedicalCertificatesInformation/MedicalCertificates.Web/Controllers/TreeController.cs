using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.TreeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IGroupService groupService;


        public TreeController(IUserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IUserService<ApplicationUser> userService, IDepartmentService departmentService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _departmentService = departmentService;
        }

        public async Task<JsonResult> GetRole()
        {
            var curUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            string role = await _userManager.IsInRoleAsync(curUser, "Admin") ? "admin" : "user";
            return Json(role);
        }

        public async Task<JsonResult> GetUserGroupsId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            List<int> groupsId = user.Groups.Select(p => p.Id).ToList();
            return Json(groupsId);
        }

        public async Task<JsonResult> GetManagementHierarchy()
        {
            var curUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            bool isAdmin = await _userManager.IsInRoleAsync(curUser, "Admin");
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
                var departmentNode = MakeDepartmentNode(department, isAdmin);
                foreach(var course in department.Courses)
                {
                    var courseNode = MakeCourseNode(course, department.Id, isAdmin );
                    foreach(var group in course.Groups)
                    {
                        var groupNode = MakeGroupNode(group, course.Id, isAdmin);
                        foreach(var student in group.Students)
                        {
                            var studentNode = MakeStudentNode(student, group.Id, isAdmin);
                            for(int i =0; i< student.MedicalCertificates.Count; i++)
                            {
                                var certificateNode = MakeCertificateNode(student.MedicalCertificates[i], ("Медицинская справка № " + (i + 1)).ToString(), student.Id, isAdmin);
                                studentNode.children.Add(certificateNode);
                            }
                            groupNode.children.Add(studentNode);
                        }
                        groupNode.children = groupNode.children.OrderBy(p => p.title).ToList();
                        courseNode.children.Add(groupNode);
                    }
                    courseNode.children = courseNode.children.OrderBy(p => p.title).ToList();
                    departmentNode.children.Add(courseNode);
                }
                departmentNode.children = departmentNode.children.OrderBy(p => p.title).ToList();
                departmentNodes.Add(departmentNode);
            }
            departmentNodes = departmentNodes.OrderBy(p => p.title).ToList();
            var json = Json(departmentNodes);
            return json;
        }

        public async Task<JsonResult> GetManagementFormHierarchy()
        {
            var curUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            bool isAdmin = await _userManager.IsInRoleAsync(curUser, "Admin");
            IReadOnlyList<Department> departments = new List<Department>();
            if (await _userManager.IsInRoleAsync(curUser, "Admin"))
            {
                departments = await _departmentService.GetAllAsync();

            }
            else
            {
                var hierarchy = _userService.GetUserManagementHierarchy(curUser);
                departments = hierarchy.Departments;
            }

            List<DepartmentNode> departmentNodes = new List<DepartmentNode>();
            foreach (var department in departments)
            {
                var departmentNode = MakeDepartmentNode(department, isAdmin);
                foreach (var course in department.Courses)
                {
                    var courseNode = MakeCourseNode(course, department.Id, isAdmin);
                    foreach (var group in course.Groups)
                    {
                        var groupNode = MakeGroupNode(group, course.Id, isAdmin);
                        courseNode.children = courseNode.children.OrderBy(p => p.title).ToList();
                        courseNode.children.Add(groupNode);
                    }
                    departmentNode.children.Add(courseNode);
                }
                departmentNode.children = departmentNode.children.OrderBy(p => p.title).ToList();
                departmentNodes.Add(departmentNode);
            }
            departmentNodes = departmentNodes.OrderBy(p => p.title).ToList();
            var json = Json(departmentNodes);
            return json;
        }

        private CertificateNode MakeCertificateNode(MedicalCertificate certificate, string title, int parentId, bool isAdmin)
        {
            CertificateNode node = new CertificateNode() { modelId = certificate.Id, parentId = parentId, title = title };
            if (isAdmin) node.userRole = "admin";
            else node.userRole = "user";
            return node;
        }

        private StudentNode MakeStudentNode(Student student, int parentId, bool isAdmin )
        {
            StudentNode node = new StudentNode() { modelId = student.Id, parentId = parentId, title = student.Surname + " "+student.Name };
            if (isAdmin) node.userRole = "admin";
            else node.userRole = "user";
            return node;
        }

        private GroupNode MakeGroupNode(Group group, int parentId, bool isAdmin)
        {
            GroupNode node = new GroupNode() { modelId = group.Id, parentId = parentId, title = group.Name };
            if (isAdmin) node.userRole = "admin";
            else node.userRole = "user";
            return node;
        }

        private CourseNode MakeCourseNode(Course course, int parentId, bool isAdmin)
        {
            CourseNode node = new CourseNode() { modelId = course.Id, parentId = parentId, title = course.Number + " курс" };
            if (isAdmin) node.userRole = "admin";
            else node.userRole = "user";
            return node;
        }

        private DepartmentNode MakeDepartmentNode(Department department, bool isAdmin)
        {
            DepartmentNode node = new DepartmentNode() { modelId = department.Id, title = department.Name };
            if (isAdmin) node.userRole = "admin";
            else node.userRole = "user";
            return node;
        }
    }
}