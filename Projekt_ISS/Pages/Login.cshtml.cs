using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_ISS.Pages
{
    public class LoginModel : PageModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
