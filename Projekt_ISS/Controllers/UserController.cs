using Microsoft.AspNetCore.Mvc;

namespace Projekt_ISS.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
