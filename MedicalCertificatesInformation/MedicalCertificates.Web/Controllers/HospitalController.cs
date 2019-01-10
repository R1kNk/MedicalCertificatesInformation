using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Service.Interfaces.Models;
using MedicalCertificates.Web.Models.HospitalViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    public class HospitalController : Controller
    {
        IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        // GET: Hospital/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            if (hospital == null) return NotFound();
            var DetailsViewModel = new DetailsHospitalViewModel() { Id = hospital.Id, Name = hospital.Name, TelephoneNumber = hospital.TelephoneNumber, MedicalCertificates = new List<MedicalCertificate>(hospital.MedicalCertificates) };
            return View(DetailsViewModel);
        }

        // GET: Hospital/Create
        public ActionResult Create()
        {
            return View();
        }

       //POST: Hospital/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHospitalViewModel model)
        {
            if(ModelState.IsValid)
            try
            {
                var hospitals = await _hospitalService.FilterAsync(p => p.Name == model.Name);
                if(hospitals!=null || hospitals.Count != 0)
                {
                        
                }
                Hospital newHospital = new Hospital() { Name = model.Name, TelephoneNumber = model.TelephoneNumber };
                var result = await _hospitalService.CreateAsync(newHospital);
                if(result!=null)
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        // GET: Hospital/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        //// POST: Hospital/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Hospital/Delete/5
        //public IActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Hospital/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}