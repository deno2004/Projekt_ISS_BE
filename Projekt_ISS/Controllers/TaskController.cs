using Microsoft.AspNetCore.Mvc;

namespace Projekt_ISS.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
