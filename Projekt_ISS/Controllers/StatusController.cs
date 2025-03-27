using Microsoft.AspNetCore.Mvc;

namespace Projekt_ISS.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
