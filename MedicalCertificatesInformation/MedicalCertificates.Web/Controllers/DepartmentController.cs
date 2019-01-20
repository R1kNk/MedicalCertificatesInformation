using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.DepartmentViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такое отделение не найдено. Обновите страницу." });

            var DetailsViewModel = _mapper.Map<DetailsDepartmentViewModel>(department);

            return View(DetailsViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Department newDepartment = _mapper.Map<Department>(model);

                    var result = await _departmentService.AddDepartmentAsync(newDepartment);
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
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такое отделение не найдено. Обновите страницу." });

            var EditViewModel = _mapper.Map<EditDepartmentViewModel>(department);
            return View(EditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDepartmentViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Department updateDepartment = _mapper.Map<Department>(model);

                    var result = await _departmentService.EditDepartmentAsync(updateDepartment);
                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        return View(model);
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
                }
                return View(model);
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такое отделение не найдено. Обновите страницу." });

            var DeleteViewModel = _mapper.Map<DeleteDepartmentViewModel>(department);

            return View(DeleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteDepartmentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCourse = await _departmentService.GetByIdAsync(model.Id);
                    if (existingCourse == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такое отделение не найдено. Обновите страницу." });

                    var result = await _departmentService.DeleteAsync(existingCourse);
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
                    case BusinessLogicResultError.DuplicateDepartmentName:
                        ModelState.AddModelError("", "Отделение с таким именем уже существует.");
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