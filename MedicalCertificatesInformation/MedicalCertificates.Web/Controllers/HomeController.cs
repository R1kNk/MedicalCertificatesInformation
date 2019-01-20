using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MedicalCertificates.Web.Models.SharedViewModels;
using MedicalCertificates.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using MedicalCertificates.DomainModel.Models;
using System.Threading.Tasks;

namespace MedicalCertificates.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
      
        public  IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
