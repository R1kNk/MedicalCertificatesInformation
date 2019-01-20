using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.BusinessServices;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Service.ReportModels;
using MedicalCertificates.Web.Models.ReportViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IDepartmentService _departmentService;
        private readonly ICourseService _courseService;


        private readonly GroupOfStudentReportService<Group, IGroupService> _groupReportService;
        private readonly GroupOfStudentReportService<Department, IDepartmentService> _departmentReportService;
        private readonly GroupOfStudentReportService<Course, ICourseService> _courseReportService;


        public ReportController(IGroupService groupService, ICourseService courseService, IDepartmentService departmentService, GroupOfStudentReportService<Group, IGroupService> groupReportService, GroupOfStudentReportService<Department, IDepartmentService> departmentReportService, GroupOfStudentReportService<Course, ICourseService> courseReportService)
        {
            _groupService = groupService;
            _departmentService = departmentService;
            _groupReportService = groupReportService;
            _courseReportService = courseReportService;
            _departmentReportService = departmentReportService;
            _courseService = courseService;
        }

        public IActionResult ConfigureGroupReport()
        {
            ConfigureGroupReportViewModel model = new ConfigureGroupReportViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigureGroupReport(ConfigureGroupReportViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(model.GroupsId==null|| model.GroupsId.Count ==0 || model.ReportType == 0|| model.SortBy == 0 || model.WhichPeopleChose == 0)
                    {
                        ModelState.AddModelError("", "Ошибка");
                        if (model.GroupsId == null)
                        {
                            ModelState.AddModelError("", "Выберите минимум 1 группу");
                        }
                        if (model.ReportType == 0)
                        {
                            ModelState.AddModelError("ReportType", "Выберите тип отчета");
                        }
                        if (model.SortBy == 0)
                        {
                            ModelState.AddModelError("SortBy", "Выберите тип сортировки");
                        }
                        if (model.WhichPeopleChose == 0)
                        {
                            ModelState.AddModelError("WhichPeopleChose", "Выберите каких студентов нужно выбрать");
                        }
                        return View(model);
                    }
                    var groups = model.GroupsId;
                    foreach(var groupId in groups)
                    {
                        var group = await _groupService.GetByIdAsync(groupId);
                        if (group == null)
                        {
                            ModelState.AddModelError("", "Одна или несколько из выбранных групп не существуют. Попробуйте ещё раз");
                            return View(model);
                        }

                    }
                    DateTime dateTime = new DateTime();
                    switch (model.ReportType)
                    {
                        case (int)ReportTypeEnum.OnADate:
                            if(!DateTime.TryParseExact(model.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                            {
                                ModelState.AddModelError("", "Ошибка");
                                ModelState.AddModelError("", "Дата для построения отчета не была выбрана");
                                return View(model);
                            }
                            break;
                        case (int)ReportTypeEnum.WithinMonths:
                            if (model.MonthsCount <= 0)
                            {
                                ModelState.AddModelError("", "Ошибка");
                                ModelState.AddModelError("", "Количество месяцев должно быть больше 0");
                                return View(model);
                            }
                            break;
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GroupReport(ConfigureGroupReportViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Group> groups = new List<Group>();
                    DateTime date = DateTime.Now;
                    GroupOfStudentsReport groupOfStudentsReport = new GroupOfStudentsReport();
                    foreach (var groupId in model.GroupsId)
                    {
                        var group = await _groupService.GetByIdAsync(groupId);
                        if (group != null)
                            groups.Add(group);

                    }

                    switch (model.ReportType)
                    {
                        case (int)ReportTypeEnum.OnADate:

                            date = DateTime.ParseExact(model.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            break;
                        case (int)ReportTypeEnum.WithinMonths:
                            date = date.AddMonths(model.MonthsCount);
                            break;
                    }
                    switch (model.WhichPeopleChose)
                    {
                        case (int)WhichPeopleChoseEnum.All:
                            groupOfStudentsReport = await _groupReportService.GetAllFromOnDateAsync(groups, date);
                            break;
                        case (int)WhichPeopleChoseEnum.InvalidCerts:
                            groupOfStudentsReport = await _groupReportService.GetInvalidOnDateFromAsync(groups, date);
                            break;
                        case (int)WhichPeopleChoseEnum.ValidCerts:
                            groupOfStudentsReport = await _groupReportService.GetValidOnDateFromAsync(groups, date);
                            break;
                        default:
                            groupOfStudentsReport = await _groupReportService.GetAllFromOnDateAsync(groups, date);
                            break;
                    }
                    switch (model.SortBy)
                    {
                        case (int)ReportSortEnum.ByGroup:
                            groupOfStudentsReport.SortByGroup();
                            break;
                        case (int)ReportSortEnum.ByDepartment: //!
                            groupOfStudentsReport.SortByDepartment();
                            break;
                        case (int)ReportSortEnum.ByCourseAscending:
                            groupOfStudentsReport.SortByCourseAscending(); //!
                            break;
                        case (int)ReportSortEnum.ByCourseDescending:
                            groupOfStudentsReport.SortByCourseDescending(); //!
                            break;
                        case (int)ReportSortEnum.ByHealthGroup:
                            groupOfStudentsReport.SortByHealthGroup(); //!
                            break;
                        case (int)ReportSortEnum.ByPhysicaleducationGroup:
                            groupOfStudentsReport.SortByPhysicalEducation(); //!
                            break;
                        case (int)ReportSortEnum.BySurnameName:
                            groupOfStudentsReport.SortBySurnameName();
                            break;
                        case (int)ReportSortEnum.ByEndDateAscending:
                            groupOfStudentsReport.SortByEndCertificateDateAscending();
                            break;
                        case (int)ReportSortEnum.ByEndDateDescending:
                            groupOfStudentsReport.SortByEndCertificateDateDescending();
                            break;
                    }
                    foreach(var group in groups)
                    {
                        groupOfStudentsReport.FromWhatContainers.Add(group.Name);
                    }
                    return View(groupOfStudentsReport);
                }
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }


        public IActionResult ConfigureDepartmentReport()
        {
            ConfigureDepartmentReportViewModel model = new ConfigureDepartmentReportViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigureDepartmentReport(ConfigureDepartmentReportViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.DepartmentsId == null || model.DepartmentsId.Count == 0 || model.ReportType == 0 || model.SortBy == 0 || model.WhichPeopleChose == 0)
                    {
                        ModelState.AddModelError("", "Ошибка");
                        if (model.DepartmentsId == null)
                        {
                            ModelState.AddModelError("", "Выберите минимум 1 отделение");
                        }
                        if (model.ReportType == 0)
                        {
                            ModelState.AddModelError("ReportType", "Выберите тип отчета");
                        }
                        if (model.SortBy == 0)
                        {
                            ModelState.AddModelError("SortBy", "Выберите тип сортировки");
                        }
                        if (model.WhichPeopleChose == 0)
                        {
                            ModelState.AddModelError("WhichPeopleChose", "Выберите каких студентов нужно выбрать");
                        }
                        return View(model);
                    }
                    var departments = model.DepartmentsId;
                    foreach (var departmentId in departments)
                    {
                        var department = await _departmentService.GetByIdAsync(departmentId);
                        if (department == null)
                        {
                            ModelState.AddModelError("", "Одно или несколько из выбранных отделений не существуют. Попробуйте ещё раз");
                            return View(model);
                        }

                    }
                    DateTime dateTime = new DateTime();
                    switch (model.ReportType)
                    {
                        case (int)ReportTypeEnum.OnADate:
                            if (!DateTime.TryParseExact(model.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                            {
                                ModelState.AddModelError("", "Ошибка");
                                ModelState.AddModelError("", "Дата для построения отчета не была выбрана");
                                return View(model);
                            }
                            break;
                        case (int)ReportTypeEnum.WithinMonths:
                            if (model.MonthsCount <= 0)
                            {
                                ModelState.AddModelError("", "Ошибка");
                                ModelState.AddModelError("", "Количество месяцев должно быть больше 0");
                                return View(model);
                            }
                            break;
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentReport(ConfigureDepartmentReportViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Department> departments = new List<Department>();
                    DateTime date = DateTime.Now;
                    GroupOfStudentsReport groupOfStudentsReport = new GroupOfStudentsReport();
                    foreach (var departmentId in model.DepartmentsId)
                    {
                        var department = await _departmentService.GetByIdAsync(departmentId);
                        if (department != null)
                            departments.Add(department);

                    }

                    switch (model.ReportType)
                    {
                        case (int)ReportTypeEnum.OnADate:

                            date = DateTime.ParseExact(model.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            break;
                        case (int)ReportTypeEnum.WithinMonths:
                            date = date.AddMonths(model.MonthsCount);
                            break;
                    }
                    switch (model.WhichPeopleChose)
                    {
                        case (int)WhichPeopleChoseEnum.All:
                            groupOfStudentsReport = await _departmentReportService.GetAllFromOnDateAsync(departments, date);
                            break;
                        case (int)WhichPeopleChoseEnum.InvalidCerts:
                            groupOfStudentsReport = await _departmentReportService.GetInvalidOnDateFromAsync(departments, date);
                            break;
                        case (int)WhichPeopleChoseEnum.ValidCerts:
                            groupOfStudentsReport = await _departmentReportService.GetValidOnDateFromAsync(departments, date);
                            break;
                        default:
                            groupOfStudentsReport = await _departmentReportService.GetAllFromOnDateAsync(departments, date);
                            break;
                    }
                    switch (model.SortBy)
                    {
                        case (int)ReportSortEnum.ByGroup:
                            groupOfStudentsReport.SortByGroup();
                            break;
                        case (int)ReportSortEnum.ByDepartment: //!
                            groupOfStudentsReport.SortByDepartment();
                            break;
                        case (int)ReportSortEnum.ByCourseAscending:
                            groupOfStudentsReport.SortByCourseAscending(); 
                            break;
                        case (int)ReportSortEnum.ByCourseDescending:
                            groupOfStudentsReport.SortByCourseDescending(); 
                            break;
                        case (int)ReportSortEnum.ByHealthGroup:
                            groupOfStudentsReport.SortByHealthGroup(); 
                            break;
                        case (int)ReportSortEnum.ByPhysicaleducationGroup:
                            groupOfStudentsReport.SortByPhysicalEducation(); 
                            break;
                        case (int)ReportSortEnum.BySurnameName:
                            groupOfStudentsReport.SortBySurnameName();
                            break;
                        case (int)ReportSortEnum.ByEndDateAscending:
                            groupOfStudentsReport.SortByEndCertificateDateAscending();
                            break;
                        case (int)ReportSortEnum.ByEndDateDescending:
                            groupOfStudentsReport.SortByEndCertificateDateDescending();
                            break;
                    }
                    foreach (var department in departments)
                    {
                        groupOfStudentsReport.FromWhatContainers.Add(department.Name);
                    }
                    return View(groupOfStudentsReport);
                }
                return View("~/Views/Shared/OperationResultNotModal.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/OperationResultNotModal.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        public IActionResult ConfigureCourseReport()
        {
            ConfigureCourseReportViewModel model = new ConfigureCourseReportViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigureCourseReport(ConfigureCourseReportViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.CoursesId == null || model.CoursesId.Count == 0 || model.ReportType == 0 || model.SortBy == 0 || model.WhichPeopleChose == 0)
                    {
                        ModelState.AddModelError("", "Ошибка");
                        if (model.CoursesId == null)
                        {
                            ModelState.AddModelError("", "Выберите минимум 1 курс");
                        }
                        if (model.ReportType == 0)
                        {
                            ModelState.AddModelError("ReportType", "Выберите тип отчета");
                        }
                        if (model.SortBy == 0)
                        {
                            ModelState.AddModelError("SortBy", "Выберите тип сортировки");
                        }
                        if (model.WhichPeopleChose == 0)
                        {
                            ModelState.AddModelError("WhichPeopleChose", "Выберите каких студентов нужно выбрать");
                        }
                        return View(model);
                    }
                    var courses = model.CoursesId;
                    foreach (var courseId in courses)
                    {
                        var course = await _courseService.GetByIdAsync(courseId);
                        if (course == null)
                        {
                            ModelState.AddModelError("", "Ошибка");
                            ModelState.AddModelError("", "Одно или несколько из выбранных отделений не существуют. Попробуйте ещё раз");
                            return View(model);
                        }

                    }
                    DateTime dateTime = new DateTime();
                    switch (model.ReportType)
                    {
                        case (int)ReportTypeEnum.OnADate:
                            if (!DateTime.TryParseExact(model.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                            {
                                ModelState.AddModelError("", "Ошибка");
                                ModelState.AddModelError("", "Дата для построения отчета не была выбрана");
                                return View(model);
                            }
                            break;
                        case (int)ReportTypeEnum.WithinMonths:
                            if (model.MonthsCount <= 0)
                            {
                                ModelState.AddModelError("", "Ошибка");
                                ModelState.AddModelError("", "Количество месяцев должно быть больше 0");
                                return View(model);
                            }
                            break;
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        //public ActionResult PrintIndex()
        //{
        //    return new ViewAsPdf("Index", new { name = "Giorgio" }) { FileName = "Test.pdf" };
        //}

        [HttpPost]
        public async Task<IActionResult> CourseReport(ConfigureCourseReportViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Course> courses = new List<Course>();
                    DateTime date = DateTime.Now;
                    GroupOfStudentsReport groupOfStudentsReport = new GroupOfStudentsReport();
                    foreach (var courseId in model.CoursesId)
                    {
                        var course = await _courseService.GetByIdAsync(courseId);
                        if (course != null)
                            courses.Add(course);

                    }

                    switch (model.ReportType)
                    {
                        case (int)ReportTypeEnum.OnADate:

                            date = DateTime.ParseExact(model.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            break;
                        case (int)ReportTypeEnum.WithinMonths:
                            date = date.AddMonths(model.MonthsCount);
                            break;
                    }
                    switch (model.WhichPeopleChose)
                    {
                        case (int)WhichPeopleChoseEnum.All:
                            groupOfStudentsReport = await _courseReportService.GetAllFromOnDateAsync(courses, date);
                            break;
                        case (int)WhichPeopleChoseEnum.InvalidCerts:
                            groupOfStudentsReport = await _courseReportService.GetInvalidOnDateFromAsync(courses, date);
                            break;
                        case (int)WhichPeopleChoseEnum.ValidCerts:
                            groupOfStudentsReport = await _courseReportService.GetValidOnDateFromAsync(courses, date);
                            break;
                        default:
                            groupOfStudentsReport = await _courseReportService.GetAllFromOnDateAsync(courses, date);
                            break;
                    }
                    switch (model.SortBy)
                    {
                        case (int)ReportSortEnum.ByGroup:
                            groupOfStudentsReport.SortByGroup();
                            break;
                        case (int)ReportSortEnum.ByDepartment: //!
                            groupOfStudentsReport.SortByDepartment();
                            break;
                        case (int)ReportSortEnum.ByCourseAscending:
                            groupOfStudentsReport.SortByCourseAscending();
                            break;
                        case (int)ReportSortEnum.ByCourseDescending:
                            groupOfStudentsReport.SortByCourseDescending();
                            break;
                        case (int)ReportSortEnum.ByHealthGroup:
                            groupOfStudentsReport.SortByHealthGroup();
                            break;
                        case (int)ReportSortEnum.ByPhysicaleducationGroup:
                            groupOfStudentsReport.SortByPhysicalEducation();
                            break;
                        case (int)ReportSortEnum.BySurnameName:
                            groupOfStudentsReport.SortBySurnameName();
                            break;
                        case (int)ReportSortEnum.ByEndDateAscending:
                            groupOfStudentsReport.SortByEndCertificateDateAscending();
                            break;
                        case (int)ReportSortEnum.ByEndDateDescending:
                            groupOfStudentsReport.SortByEndCertificateDateDescending();
                            break;
                    }
                    //
                    foreach(var course in courses)
                    {
                        groupOfStudentsReport.FromWhatContainers.Add(course.Number + "-й курс - " + course.Department.Name);
                    }
                    return View(groupOfStudentsReport);
                }
                return View("~/Views/Shared/OperationResultNotModal.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/OperationResultNotModal.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }



    }
}