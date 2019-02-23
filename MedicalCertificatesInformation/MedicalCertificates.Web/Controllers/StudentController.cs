using System;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using MedicalCertificates.Web.Models.StudentViewModels;
using MedicalCertificates.Web.Models.TreeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;


        public StudentController(IStudentService studentService, IGroupService groupService, IMapper mapper)
        {
            _studentService = studentService;
            _groupService = groupService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var DetailsViewModel = _mapper.Map<DetailsStudentViewModel>(student);

            return View(DetailsViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if(group == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

            var CreateViewModel = new CreateStudentViewModel();
            CreateViewModel.GroupId = group.Id;

            return View(CreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateStudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student newStudent = _mapper.Map<Student>(model);

                    var result = await _studentService.AddStudentAsync(newStudent, model.GroupId);
                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        return View(model);
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create, node: new GenericNode(newStudent.Surname + " " + newStudent.Name, newStudent.Id, true, "admin", "student", true, newStudent.GroupId)));
                }
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var EditViewModel = _mapper.Map<EditStudentViewModel>(student);
            return View(EditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditStudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student updateStudent = _mapper.Map<Student>(model);

                    if (updateStudent.BirthDate > DateTime.Now)
                    {
                        ModelState.AddModelError("BirthDate", "Дата рождения не должна быть больше текущей");
                        return View(model);
                    }

                    var existingStudent = await _studentService.GetByIdAsync(updateStudent.Id);
                    if (existingStudent == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

                    existingStudent.Name = updateStudent.Name;
                    existingStudent.Surname = updateStudent.Surname;
                    existingStudent.SecondName = updateStudent.SecondName;
                    existingStudent.BirthDate = DateTime.ParseExact(model.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    var result = await _studentService.UpdateAsync(existingStudent);
                    if(result.IsSucceed)
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit, node: new GenericNode(existingStudent.Surname + " " + existingStudent.Name, existingStudent.Id, true, "admin", "student", true, existingStudent.GroupId)));
                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if(student == null)
               return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var DeleteViewModel = _mapper.Map<DeleteStudentViewModel>(student);
           
            return View(DeleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteStudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingStudent = await _studentService.GetByIdAsync(model.Id);
                    if (existingStudent == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

                    var result = await _studentService.DeleteAsync(existingStudent);
                    if(result.IsSucceed)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Delete));

                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Move(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var MoveViewModel = _mapper.Map<MoveStudentViewModel>(student);

            return View(MoveViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Move(MoveStudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _studentService.MoveStudentAsync(model.Id, model.GroupId);
                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        return View(model);
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Move));
                }
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Move, "Произошла неизвестная ошибка"));
            }
        }

        private void AddOperationResultErrorsToModelState(OperationResult<BusinessLogicResultError> operationResult)
        {
            foreach (var businessLogicError in operationResult.Errors)
            {
                switch (businessLogicError)
                {
                    case BusinessLogicResultError.GroupNotFound:
                        ModelState.AddModelError("", "Такая группа не найдена. Обновите страницу");
                        break;
                    case BusinessLogicResultError.StudentNotFound:
                        ModelState.AddModelError("", "Такой студент не найден. Обновите страницу");
                        break;
                    case BusinessLogicResultError.AlreadyInThisGroup:
                        ModelState.AddModelError("", "Студент уже в этой группе.");
                        break;
                    default:
                        ModelState.AddModelError("", "Неизвестная ошибка");
                        break;
                }
            }
        }
    }
}