using AutoMapper;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.MedicalCertificatesViewModels;
using MedicalCertificates.Web.Models.SharedEntities;
using MedicalCertificates.Web.Models.SharedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    public class MedicalCertificateController : Controller
    {

        private readonly IMedicalCertificateService _medicalCertificateService;
        private readonly IStudentService _studentService;
        private readonly IPhysicalEducationService _physicalEducationService;
        private readonly IHealthGroupService _healthGroupService;
        private readonly IHospitalService _hospitalService;


        private readonly IMapper _mapper;

        public MedicalCertificateController(IMedicalCertificateService medicalCertificateService, IStudentService studentService, IPhysicalEducationService physicalEducationService, IHealthGroupService healthGroupService, IHospitalService hospitalService, IMapper mapper)
        {
            _medicalCertificateService = medicalCertificateService;
            _studentService = studentService;
            _physicalEducationService = physicalEducationService;
            _healthGroupService = healthGroupService;
            _hospitalService = hospitalService;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var medicalCertificate = await _medicalCertificateService.GetByIdAsync(id);
            if (medicalCertificate == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая справка не найдена. Обновите страницу." });
            var DetailsViewModel = _mapper.Map<DetailsMedicalCertificatesViewModel>(medicalCertificate);
            DetailsViewModel.CertificateTerm = DetailsViewModel.CertificateTerm / 12;
            return View(DetailsViewModel);
        }

        public async Task<IActionResult> Create(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if(student == null)
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такой студент не найден. Обновите страницу." });

            var CreateViewModel = new CreateMedicalCertificateViewModel();
            CreateViewModel.StudentId = student.Id;
            CreateViewModel.HealthGroups = await GetAllHealthGroupsAsync();
            CreateViewModel.PhysicalEducations = await GetAllPhysicalEducationsAsync();
            CreateViewModel.Hospitals = await GetAllHospitalsAsync();
            CreateViewModel.Terms = GetCertificateTerms();

            return View(CreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMedicalCertificateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MedicalCertificate newMedicalCertificate = _mapper.Map<MedicalCertificate>(model);
                    if (model.IsUsingTerm)
                    {
                        if(model.CertificateTerm == 0)
                        {
                            ModelState.AddModelError("", "Выберите срок действия!");
                            model.HealthGroups = await GetAllHealthGroupsAsync();
                            model.PhysicalEducations = await GetAllPhysicalEducationsAsync();
                            model.Hospitals = await GetAllHospitalsAsync();
                            model.Terms = GetCertificateTerms();
                            model.IsUsingTerm = false;
                            return View(model);
                        }
                        newMedicalCertificate.FinishDate = newMedicalCertificate.StartDate.AddMonths(model.CertificateTerm);
                        newMedicalCertificate.CertificateTerm = newMedicalCertificate.FinishDate.Subtract(newMedicalCertificate.StartDate).TotalDays;
                    }
                    else
                    {
                        if(model.FinishDate == "")
                        {
                            ModelState.AddModelError("", "Выберите дату окончания справки!");
                            model.HealthGroups = await GetAllHealthGroupsAsync();
                            model.PhysicalEducations = await GetAllPhysicalEducationsAsync();
                            model.Hospitals = await GetAllHospitalsAsync();
                            model.Terms = GetCertificateTerms();
                            model.IsUsingTerm = false;
                            return View(model);
                        }
                        newMedicalCertificate.FinishDate = DateTime.ParseExact(model.FinishDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        newMedicalCertificate.CertificateTerm = newMedicalCertificate.FinishDate.Subtract(newMedicalCertificate.StartDate).TotalDays;
                    }
                    var result = await _medicalCertificateService.AddMedicalCertificateAsync(newMedicalCertificate, model.StudentId);

                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        model.HealthGroups = await GetAllHealthGroupsAsync();
                        model.PhysicalEducations = await GetAllPhysicalEducationsAsync();
                        model.Hospitals = await GetAllHospitalsAsync();
                        model.Terms = GetCertificateTerms();
                        model.IsUsingTerm = false;
                        return View(model);
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Create));
                }
                model.HealthGroups = await GetAllHealthGroupsAsync();
                model.PhysicalEducations = await GetAllPhysicalEducationsAsync();
                model.Hospitals = await GetAllHospitalsAsync();
                model.Terms = GetCertificateTerms();
                model.IsUsingTerm = false;
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Create, "Произошла неизвестная ошибка"));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var certificate = await _medicalCertificateService.GetByIdAsync(id);
            if(certificate==null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая справка не найдена. Обновите страницу." });

            var EditViewModel =  _mapper.Map<EditMedicalCertificatesViewModel>(certificate);

            EditViewModel.HealthGroups = await GetAllHealthGroupsAsync();
            EditViewModel.PhysicalEducations = await GetAllPhysicalEducationsAsync();
            EditViewModel.Hospitals = await GetAllHospitalsAsync();
            EditViewModel.Terms = GetCertificateTerms();
            return View(EditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMedicalCertificatesViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MedicalCertificate updateMedicalCertificate = _mapper.Map<MedicalCertificate>(model);
                    if (model.IsUsingTerm)
                    {
                        updateMedicalCertificate.FinishDate = updateMedicalCertificate.StartDate.AddMonths(model.CertificateTerm);
                        updateMedicalCertificate.CertificateTerm = updateMedicalCertificate.FinishDate.Subtract(updateMedicalCertificate.StartDate).TotalDays;
                    }
                    else
                    {
                        updateMedicalCertificate.FinishDate = DateTime.ParseExact(model.FinishDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        updateMedicalCertificate.CertificateTerm = updateMedicalCertificate.FinishDate.Subtract(updateMedicalCertificate.StartDate).TotalDays;
                    }
                    var result = await _medicalCertificateService.EditMedicalCertificateAsync(updateMedicalCertificate);
                    if (!result.IsSucceed)
                    {
                        AddOperationResultErrorsToModelState(result);
                        model.HealthGroups = await GetAllHealthGroupsAsync();
                        model.PhysicalEducations = await GetAllPhysicalEducationsAsync();
                        model.Hospitals = await GetAllHospitalsAsync();
                        model.Terms = GetCertificateTerms();
                        model.IsUsingTerm = false;
                        return View(model);
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Edit));
                }
                model.HealthGroups = await GetAllHealthGroupsAsync();
                model.PhysicalEducations = await GetAllPhysicalEducationsAsync();
                model.Hospitals = await GetAllHospitalsAsync();
                model.Terms = GetCertificateTerms();
                model.IsUsingTerm = false;
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Edit, "Произошла неизвестная ошибка"));
            }
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var certificate = await _medicalCertificateService.GetByIdAsync(id);
            if (certificate == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая справка не найдена. Обновите страницу." });

            var DeleteViewModel = _mapper.Map<DeleteMedicalCertificateViewModel>(certificate);

            return View(DeleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteMedicalCertificateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _medicalCertificateService.DeleteMedicalCertificateAsync(model.Id);
                    if (!result.IsSucceed)
                    {
                        foreach(var error in result.Errors)
                        {
                            switch (error)
                            {
                                case BusinessLogicResultError.CertificateNotFound:
                                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Такая справка не найдена. Обновите страницу"));
                                case BusinessLogicResultError.ExpiredCertificate:
                                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Эта справка не является актуальной. Её изменение или удаление приведет к нарушению статистики о справках студента"));
                                default:
                                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка. Обновите страницу."));
                            }
                        }
                    }
                    return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(true, OperationResultEnum.Delete));
                }
                return View(model);

            }
            catch
            {
                return View("~/Views/Shared/OperationResult.cshtml", new OperationResultViewModel(false, OperationResultEnum.Delete, "Произошла неизвестная ошибка"));
            }
        }

        private async Task<IReadOnlyList<HealthGroup>> GetAllHealthGroupsAsync()
        {
            return await _healthGroupService.GetAllAsync();
        }

        private async Task<IReadOnlyList<PhysicalEducation>> GetAllPhysicalEducationsAsync()
        {
            return await _physicalEducationService.GetAllAsync();
        }

        private async Task<IReadOnlyList<Hospital>> GetAllHospitalsAsync()
        {
            return await _hospitalService.GetAllAsync();
        }

        private void AddOperationResultErrorsToModelState(OperationResult<BusinessLogicResultError> operationResult)
        {
            foreach (var businessLogicError in operationResult.Errors)
            {
                switch (businessLogicError)
                {
                    case BusinessLogicResultError.CertificateNotFound:
                        ModelState.AddModelError("", "Такая справка не найдена. Обновите страницу");
                        break;
                    case BusinessLogicResultError.ExpiredCertificate:
                        ModelState.AddModelError("", "Эта справка не является актуальной. Её изменение или удаление приведет к нарушению статистики о справках студента");
                        break;
                    case BusinessLogicResultError.InvalidDate:
                        ModelState.AddModelError("", "Даты справки указаны некорректно");
                        break;
                    case BusinessLogicResultError.NoCertificates:
                        ModelState.AddModelError("", "У этого студента нет справок");
                        break;
                    case BusinessLogicResultError.OverlappingDate:
                        ModelState.AddModelError("", "Указанные даты не являются корректными по отношению к уже имеющимся справкам");
                        break;
                    case BusinessLogicResultError.StudentNotFound:
                        ModelState.AddModelError("", "Такой студент не найден. Обновите страницу");
                        break;
                    default:
                        ModelState.AddModelError("", "Произошла неизвестная ошибка");
                        break;
                }
            }
        }
        
        private List<CertificateTerm> GetCertificateTerms()
        {
            return new List<CertificateTerm>()
            {
                new CertificateTerm() { Months = 1, Text="1 месяц"},
                new CertificateTerm() { Months = 3, Text="3 месяц"},
                new CertificateTerm() { Months = 6, Text="6 месяцев"},
                new CertificateTerm() { Months = 12, Text="12 месяцев"}
            };
        }
    }
}