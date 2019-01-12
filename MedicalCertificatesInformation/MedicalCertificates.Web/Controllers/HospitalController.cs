using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.HospitalViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HospitalController : Controller
    {
        IHospitalService _hospitalService;
        IMapper _mapper;

        public HospitalController(IHospitalService hospitalService, IMapper mapper)
        {
            _hospitalService = hospitalService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var hospitals = await _hospitalService.GetAllAsync();
            hospitals = hospitals.OrderBy(p => p.Id).ToList();
            return View(hospitals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая поликлиника не найдена. Обновите страницу." });
            var DetailsViewModel =  _mapper.Map<DetailsHospitalViewModel>(hospital);
            return View(DetailsViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create(CreateHospitalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hospital = await _hospitalService.GetSingleOrDefaultAsync(p => p.Name == model.Name);
                    if (hospital != null)
                    {
                        ModelState.AddModelError("Name", "Поликлиника с таким именем уже существует!");
                        return View(model);
                    }
                    Hospital newHospital = _mapper.Map<Hospital>(model);
                    var result = await _hospitalService.CreateAsync(newHospital);
                    if (result != null)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
            }    
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
            return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая поликлиника не найдена. Обновите страницу." });

            var editHospitalViewModel = _mapper.Map<EditHospitalViewModel>(hospital);
            return View(editHospitalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditHospitalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hospital = await _hospitalService.GetSingleOrDefaultAsync(p => p.Id == model.Id);
                    if (hospital == null)
                    {
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая поликлиника не найдена. Обновите страницу." });
                    }
                    hospital.Name = model.Name;
                    hospital.TelephoneNumber = model.TelephoneNumber;
                    var result = await _hospitalService.UpdateAsync(hospital);
                    if (result.IsSucceed)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit,"Произошла неизвестная ошибка"));
            }
            return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null)
               return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });

            var deleteViewModel = _mapper.Map<DeleteHospitalViewModel>(hospital);
            return View(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteHospitalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hospital = await _hospitalService.GetByIdAsync(model.Id);
                    if (hospital == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription= "Такая поликлиника не найдена. Обновите страницу."});
                    var result = await _hospitalService.DeleteAsync(hospital);
                    if (result.IsSucceed)
                        return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Delete));
                }
            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
            }
            return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
        }
    }
}