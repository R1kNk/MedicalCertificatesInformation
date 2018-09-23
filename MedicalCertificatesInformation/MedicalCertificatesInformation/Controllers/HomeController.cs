using MedicalCertificatesInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalCertificatesInformation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MedicalCertificatesContext context = new MedicalCertificatesContext();
            context.Departments.Add(new Models.Database.Models.Department() { Name = "So?" });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}