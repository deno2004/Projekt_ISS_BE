using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt_ISS.Data;

namespace Projekt_ISS.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TaskTrackerContext? _context;

        //povezava z bazo
        public LoginModel(TaskTrackerContext? context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Here you would typically check the user's credentials against the database
            // For example:
            // var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            // if (user != null)
            // {
            //     return RedirectToPage("/Index");
            // }
            return Page();
        }

        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
