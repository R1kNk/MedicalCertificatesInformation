using AutoMapper;
using MedicalCertificates.Service.Interfaces.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCertificates.Web.Controllers
{
    [Authorize]
    public class MedicalCertificateController : Controller
    {
        private readonly IMedicalCertificateService _medicalCertificateService;
        private readonly IMapper _mapper;

        public MedicalCertificateController(IMedicalCertificateService medicalCertificateService, IMapper mapper)
        {
            _medicalCertificateService = medicalCertificateService;
            _mapper = mapper;
        }
        //// GET: MedicalCertificate
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: MedicalCertificate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedicalCertificate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalCertificate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicalCertificate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedicalCertificate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicalCertificate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedicalCertificate/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}