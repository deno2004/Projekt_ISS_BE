using Microsoft.AspNetCore.Mvc;

namespace Projekt_ISS.Controllers
{
    public class DeadlineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
