﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models;
using MedicalCertificates.Web.Models.PhysicalEducationViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhysicalEducationController : Controller
    {
        IPhysicalEducationService _physicalEducationService;
        IMapper _mapper;

        public PhysicalEducationController(IPhysicalEducationService physicalEducationService, IMapper mapper)
        {
            _physicalEducationService = physicalEducationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var physicalEducations = await _physicalEducationService.GetAllAsync();
            physicalEducations = physicalEducations.OrderBy(p => p.Id).ToList();
            return View(physicalEducations);
        }

        public async Task<IActionResult> Details(int id)
        {
            PhysicalEducation physicalEducation = await _physicalEducationService.GetByIdAsync(id);
            if (physicalEducation == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });
            var DetailsViewModel = _mapper.Map<DetailsPhysicalEducationViewModel>(physicalEducation);
            return View(DetailsViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhysicalEducationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var physicalEducation = await _physicalEducationService.GetSingleOrDefaultAsync(p => p.Name == model.Name);
                    if (physicalEducation != null)
                    {
                        ModelState.AddModelError("Name", "Группа по физкультуре с таким именем уже существует!");
                        return View(model);
                    }
                    var newPhysicalEducation = _mapper.Map<PhysicalEducation>(model);
                    var result = await _physicalEducationService.CreateAsync(newPhysicalEducation);
                    if (result != null)
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var physicalEducation = await _physicalEducationService.GetByIdAsync(id);
            if (physicalEducation == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });
            var editPhysicalEducationViewModel = _mapper.Map<EditPhysicaleducationViewModel>(physicalEducation);
            return View(editPhysicalEducationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPhysicaleducationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var physicalEducation = await _physicalEducationService.GetSingleOrDefaultAsync(p => p.Id == model.Id);
                    if (physicalEducation == null)
                    {
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });
                    }
                    physicalEducation.Name = model.Name;
                    var result = await _physicalEducationService.UpdateAsync(physicalEducation);
                    if (result.IsSucceed)
                     return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var physicalEducation = await _physicalEducationService.GetByIdAsync(id);
            if (physicalEducation == null) return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая группа по физкультуре не найдена. Обновите страницу." });
            var deleteViewModel = _mapper.Map<DeletePhysicalEducationViewModel>(physicalEducation);
            return View(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeletePhysicalEducationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var physicalEducation = await _physicalEducationService.GetByIdAsync(model.Id);
                    if (physicalEducation == null)
                    {
                        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel() { MessageDescription = "Такая поликлиника не найдена. Обновите страницу." });
                    }
                    var result = await _physicalEducationService.DeleteAsync(physicalEducation);
                    if (result.IsSucceed)
                        return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
    }
}