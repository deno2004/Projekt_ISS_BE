using Microsoft.AspNetCore.Mvc;

namespace Projekt_ISS.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
