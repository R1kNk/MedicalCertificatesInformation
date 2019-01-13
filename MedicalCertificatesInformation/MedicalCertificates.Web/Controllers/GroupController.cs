using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.GroupViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, ICourseService courseService, IMapper mapper)
        {
            _groupService = groupService;
            _courseService = courseService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Details(int id)
        {
            var group = await _groupService.GetByIdAsync(id);

            if(group == null)
               return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

            var DetailsViewModel = _mapper.Map<DetailsGroupViewModel>(group);

            return View(DetailsViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой курс не найден. Обновите страницу." });

            var CreateViewModel = new CreateGroupViewModel();
            CreateViewModel.CourseId = course.Id;

            return View(CreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create (CreateGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Group newGroup = _mapper.Map<Group>(model);

                    var result = await _groupService.AddGroupAsync(newGroup, model.CourseId);
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

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var group = _groupService.GetByIdAsync(id);
            if (group == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

            var EditViewModel = _mapper.Map<EditGroupViewModel>(group);
            return View(EditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Group updateGroup = _mapper.Map<Group>(model);

                    var existingGroup = await _groupService.GetByIdAsync(updateGroup.Id);
                    if (existingGroup == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

                    existingGroup.Name = updateGroup.Name;
                    var result = await _groupService.UpdateAsync(existingGroup);
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

            var DeleteViewModel = _mapper.Map<DeleteGroupViewModel>(group);

            return View(DeleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingGroup = await _groupService.GetByIdAsync(model.Id);
                    if (existingGroup == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа не найдена. Обновите страницу." });

                    var result = await _groupService.DeleteAsync(existingGroup);
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

        [Authorize(Roles = "Admin")]
        public async Task<bool> Move(int groupId, int courseId)
        {
            var group = await _groupService.GetByIdAsync(groupId);
            if (group == null)
                return false;
            var course = await _courseService.GetByIdAsync(courseId);
            if (course == null)
                return false;
            group.CourseId = course.Id;
            var result = await _groupService.UpdateAsync(group);
            if (result.IsSucceed)
                return true;

            return false;
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
                    case BusinessLogicResultError.CourseNotFound:
                        ModelState.AddModelError("", "Такой курс не найден. Обновите страницу");
                        break;
                    case BusinessLogicResultError.DuplicateGroupName:
                        ModelState.AddModelError("", "Группа с таким именем уже существует.");
                        break;
                    default:
                        ModelState.AddModelError("", "Неизвестная ошибка");
                        break;
                }
            }
        }

    }
}