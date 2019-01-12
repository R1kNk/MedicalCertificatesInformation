using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using MedicalCertificates.Web.Models.StudentViewModels;
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
        //// GET: Student
        //public Task<IActionResult> Index()
        //{
        //    return View();
        //}

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var DetailsViewModel = _mapper.Map<DetailsStudentViewModel>(student);

            return View(DetailsViewModel);
        }

        // GET: Student/Create
        public async Task<IActionResult> Create(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if(group == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

            var CreateViewModel = new CreateStudentViewModel();
            CreateViewModel.GroupId = group.Id;

            return View(CreateViewModel);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                }

                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        // GET: Student/Edit/5
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetByIdAsync(id);
            if (student == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var EditViewModel = _mapper.Map<EditStudentViewModel>(student);
            return View(EditViewModel);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditStudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student updateStudent = _mapper.Map<Student>(model);

                    var existingStudent = await _studentService.GetByIdAsync(updateStudent.Id);
                    if (existingStudent == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

                    existingStudent.Name = updateStudent.Name;
                    existingStudent.Surname = updateStudent.Surname;
                    await _studentService.UpdateAsync(existingStudent);
                }
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }
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

        // POST: Student/Delete/5
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

                    await _studentService.DeleteAsync(existingStudent);
                }
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Delete));
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
            }
        }

        // GET: Student/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Move(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var MoveViewModel = _mapper.Map<MoveStudentViewModel>(student);

            return View(MoveViewModel);
        }

        // POST: Student/Create
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
                }

                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Move));
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