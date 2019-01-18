using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.CourseViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly IReadOnlyList<int> courseNumbers;

        public CourseController(IDepartmentService departmentService, ICourseService courseService, IMapper mapper)
        {
            _departmentService = departmentService;
            _courseService = courseService;
            _mapper = mapper;
            courseNumbers = new List<int>
            {
                1,
                2,
                3,
                4
            };
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой курс не найден. Обновите страницу." });

            var DetailsViewModel = _mapper.Map<DetailsCourseViewModel>(course);

            return View(DetailsViewModel);
        }

        public async Task<IActionResult> Create(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такое отделение не найдено. Обновите страницу." });

            var CreateViewModel = new CreateCourseViewModel();
            CreateViewModel.DepartmentId = department.Id;
            CreateViewModel.CourseNumbers = courseNumbers;

            return View(CreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Course newCourse = _mapper.Map<Course>(model);

                    var result = await _courseService.AddCourseAsync(newCourse, model.DepartmentId);
                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        return View(model);
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

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой курс не найден. Обновите страницу." });

            var EditViewModel = _mapper.Map<EditCourseViewModel>(course);
            EditViewModel.CourseNumbers = courseNumbers;

            return View(EditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Course updateCourse = _mapper.Map<Course>(model);

                    var existingCourse = await _courseService.GetByIdAsync(updateCourse.Id);
                    if (existingCourse == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой курс не найден. Обновите страницу." });

                    var coursesWithSameNumber = existingCourse.Department.Courses.Where(p => p.Id != existingCourse.Id && p.Number == model.Number).ToList();
                    if(coursesWithSameNumber ==null || coursesWithSameNumber.Count!=0)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Курс с таким номером уже существует в этом отделении" });

                    existingCourse.Number = updateCourse.Number;
                    var result = await _courseService.UpdateAsync(existingCourse);
                    if (result.IsSucceed)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой курс не найден. Обновите страницу." });

            var DeleteViewModel = _mapper.Map<DeleteCourseViewModel>(course);

            return View(DeleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteCourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCourse = await _courseService.GetByIdAsync(model.Id);
                    if (existingCourse == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой курс не найден. Обновите страницу." });

                    var result = await _courseService.DeleteAsync(existingCourse);
                    if (result.IsSucceed)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Delete));
                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
            }
            return View(model);
        }
        
        private void AddOperationResultErrorsToModelState(OperationResult<BusinessLogicResultError> operationResult)
        {
            foreach (var businessLogicError in operationResult.Errors)
            {
                switch (businessLogicError)
                {
                    case BusinessLogicResultError.CourseNotFound:
                        ModelState.AddModelError("", "Такой курс не найден. Обновите страницу");
                        break;
                    case BusinessLogicResultError.DuplicateCourseNumber:
                        ModelState.AddModelError("", "Курс с таким номером уже существует в данном отделении.");
                        break;
                    case BusinessLogicResultError.DepartmentNotFound:
                        ModelState.AddModelError("", "Такое отделение не найдено. Обновите страницу.");
                        break;
                    default:
                        ModelState.AddModelError("", "Неизвестная ошибка");
                        break;
                }
            }
        }
    }
}