using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt_ISS.Data;
using Projekt_ISS.Models;

namespace Projekt_ISS.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly TaskTrackerContext? _context;

        public RegisterModel(TaskTrackerContext? context)
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
            // Here you would typically save the user to the database
            // For example:
            // _context.Users.Add(User);
            // _context.SaveChanges();
            return RedirectToPage("/Index");
        }

        public User? User { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
