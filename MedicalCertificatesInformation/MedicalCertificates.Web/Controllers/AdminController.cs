using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.AccountViewModels;
using MedicalCertificates.Web.Models.AdminViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IUserManager<ApplicationUser> _userManager;
        private readonly ISignInManager<ApplicationUser> _signInManager;
        private readonly IStringConverterService _converter;
        private readonly ILogger _logger;
        private readonly IUserService<ApplicationUser> _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGroupService _groupService;
        private readonly IDepartmentService _departmentService;


        public AdminController(
            IUserManager<ApplicationUser> userManager,
            ISignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IStringConverterService converter,
            IUserService<ApplicationUser> userService,
            IHttpContextAccessor httpContextAccessor,
            IGroupService groupService,
            IDepartmentService departmentService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
            _converter = converter;
            _httpContextAccessor = httpContextAccessor;
            _groupService = groupService;
            _departmentService = departmentService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;
                if(model.IsDepartmentManager)
                {
                    user = new DepartmentManagerUser { UserName = _converter.ConvertToUsername(_converter.ConvertFromRussianToEnglish(model.Username)), Pseudonim = model.Username, Email = "office@kbp.by" };
                }
                else
                {
                    user = new DefaultUser { UserName = _converter.ConvertToUsername(_converter.ConvertFromRussianToEnglish(model.Username)), Pseudonim = model.Username, Email = "office@kbp.by" };
                }
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    _logger.LogInformation("User created a new account with password.");

                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
                AddErrors(result);
            }

            return View(model);
        }

        public async Task<IActionResult> Users()
        {
            var User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            IReadOnlyList<ApplicationUser> users = _userManager.Users.Where(p => p.Id != User.Id).ToList();
            return View(users);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });

            DeleteUserViewModel model = new DeleteUserViewModel();
            model.Id = id;
            model.Username = user.Pseudonim;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });
                    var result = await _userService.DeleteAsync(user);
                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        return View(model);
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Delete));
                }
                return View(model);
            }
            catch(Exception e)
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
            }

        }


        public async Task<IActionResult> EditUserGroups(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });

            if (!(user is DefaultUser))
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Данный пользователь не может иметь подконтролных групп. Обновите страницу" });

            EditUserGroupsViewModel model = new EditUserGroupsViewModel();
            model.UserId = id;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUserGroups([FromBody] EditUserGroupsViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });
                if (!(user is DefaultUser))
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Данный пользователь не может иметь подконтролных групп. Обновите страницу" });
     
                var result = await _groupService.EditUserGroupsAsync(model.UserId, model.ActiveGroupsId, model.InactiveGroupId);
                if(result.IsSucceed)
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                return View(model);
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }

        }

        public async Task<IActionResult> EditManagerUserDepartment(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });

            if (!(user is DepartmentManagerUser))
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Данный пользователь не может иметь подконтрольное отделение. Обновите страницу" });

            EditManagerUserDepartmentViewModel model = new EditManagerUserDepartmentViewModel();
            model.UserId = id;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditManagerUserDepartment([FromBody] EditManagerUserDepartmentViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });
                if (!(user is DepartmentManagerUser))
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Данный пользователь не может иметь подконтрольное отделение. Обновите страницу" });
                if(model.DepartmentsId != null && model.DepartmentsId.Count > 1)
                {
                    ModelState.AddModelError("", "Выберите не больше одного отделения");
                    return View(model);
                }
                var result = await _departmentService.EditManagerDepartmentAsync(model.UserId, model.DepartmentsId);
                if (result.IsSucceed)
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
                else
                {
                    AddOperationResultErrorsToModelState(result);
                }
                return View(model);
            }
            catch(Exception e)
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditUserName(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой пользователь не найден. Обновите страницу." });

            EditUserNameViewModel model = new EditUserNameViewModel();
            model.UserId = id;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserName(EditUserNameViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.SetPseudonimAsync(model.UserId, model.Username);
                    if (!result.IsSucceed)
                    {

                        AddOperationResultErrorsToModelState(result);
                        return View(model);
                    }
                }
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }
          
        }

        
        private void AddOperationResultErrorsToModelState(OperationResult<IdentityResultError> operationResult)
        {
            foreach (var identityError in operationResult.Errors)
            {
                switch (identityError)
                {
                    case IdentityResultError.UserNotFound:
                        ModelState.AddModelError("", " Такой пользователь не найден. Обновите страницу.");
                        break;
                    case IdentityResultError.DuplicateUserName:
                        ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
                        break;
                    default:
                        ModelState.AddModelError("", "Неизвестная ошибка");
                        break;
                }
            }
        }


        private void AddOperationResultErrorsToModelState(OperationResult<BusinessLogicResultError> operationResult)
        {
            foreach (var identityError in operationResult.Errors)
            {
                switch (identityError)
                {
                    case BusinessLogicResultError.UserNotFound:
                        ModelState.AddModelError("", " Такой пользователь не найден. Обновите страницу.");
                        break;
                    case BusinessLogicResultError.InvalidUserType:
                        ModelState.AddModelError("", "Данный пользователь не может иметь подконтрольные отделения");
                        break;
                    default:
                        ModelState.AddModelError("", "Неизвестная ошибка");
                        break;
                }
            }
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if(error.Code == "DuplicateUserName")
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
                }
                else
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}