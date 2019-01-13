using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models;
using MedicalCertificates.Web.Models.HealthGroupViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HealthGroupController : Controller
    {
        private readonly IHealthGroupService _healthGroupService;
        private readonly IMapper _mapper;

        public HealthGroupController(IHealthGroupService healthGroupService, IMapper mapper)
        {
            _healthGroupService = healthGroupService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var healthGroups = await _healthGroupService.GetAllAsync();

            healthGroups = healthGroups.OrderBy(p => p.Id).ToList();
            return View(healthGroups);
        }

        public async Task<IActionResult> Details(int id)
        {
            var healthGroup = await _healthGroupService.GetByIdAsync(id);
            if (healthGroup == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });

            var DetailsViewModel = _mapper.Map<DetailsHealthGroupViewModel>(healthGroup);
            return View(DetailsViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHealthGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var healthGroup = await _healthGroupService.GetSingleOrDefaultAsync(p => p.Name == model.Name);
                    if (healthGroup != null)
                    {
                        ModelState.AddModelError("Name", "Группа по физкультуре с таким именем уже существует!");
                        return View(model);
                    }
                    var newHealthGroup = _mapper.Map<HealthGroup>(model);
                    var result = await _healthGroupService.CreateAsync(newHealthGroup);
                    if (result != null)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var healthGroup = await _healthGroupService.GetByIdAsync(id);

            if (healthGroup == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });

            var edithealthGroupViewModel = _mapper.Map<EditHealthGroupViewModel>(healthGroup);
            return View(edithealthGroupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditHealthGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var healthGroup = await _healthGroupService.GetSingleOrDefaultAsync(p => p.Id == model.Id);

                    if (healthGroup == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });
                    healthGroup.Name = model.Name;
                    var result = await _healthGroupService.UpdateAsync(healthGroup);

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
            var healthGroup = await _healthGroupService.GetByIdAsync(id);

            if (healthGroup == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });

            var deleteViewModel = _mapper.Map<DeleteHealthGroupViewModel>(healthGroup);
            return View(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteHealthGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var healthGroup = await _healthGroupService.GetByIdAsync(model.Id);
                    if (healthGroup == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая поликлиника не найдена. Обновите страницу." });
                    var result = await _healthGroupService.DeleteAsync(healthGroup);
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
    }
}