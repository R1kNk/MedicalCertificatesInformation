using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedicalCertificates.Web.Models.AccountViewModels;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Web.Models.SharedViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUserManager<ApplicationUser> _userManager;
        private readonly ISignInManager<ApplicationUser> _signInManager;
        private readonly IStringConverterService _converter;
        private readonly ILogger _logger;
        private readonly IUserService<ApplicationUser> _userService;

        public AccountController(
            IUserManager<ApplicationUser> userManager,
            ISignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IStringConverterService converter,
            IUserService<ApplicationUser> userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
            _converter = converter;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(/*string returnUrl = null*/)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //ViewData["ReturnUrl"] = returnUrl;
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Users = await _userService.GetAllUsersAsync();
            return View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model/*, string returnUrl = null*/)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal("/Home/Index");
                    //return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неправильное имя пользователя или пароль");
                    model.Users = await _userService.GetAllUsersAsync();
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

       

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

       
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
