using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;

namespace MyPortfolio.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;
        public LoginModel(AppDbContext db) { _db = db; }

        public string ErrorMessage { get; set; } = string.Empty;
        public string InfoMessage { get; set; } = string.Empty;

        public void OnGet(string? msg)
        {
            if (msg == "registered") InfoMessage = "Account created! Please login.";
        }

        public IActionResult OnPost(string Email, string Password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user == null)
            {
                ErrorMessage = "Invalid email or password. Please try again.";
                return Page();
            }

            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetInt32("UserID", user.ID);

            if (user.Role == "Admin")
                return RedirectToPage("/Admin/Dashboard");

            return RedirectToPage("/Index");
        }
    }
}
