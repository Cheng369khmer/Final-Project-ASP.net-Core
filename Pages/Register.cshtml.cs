using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _db;
        public RegisterModel(AppDbContext db) { _db = db; }

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string Name, string Email, string Gender, string Password, string Remark)
        {
            if (_db.Users.Any(u => u.Email == Email))
            {
                ErrorMessage = "This email is already registered. Please use a different email.";
                return Page();
            }

            var user = new UserModel
            {
                Name = Name,
                Email = Email,
                Gender = Gender,
                Password = Password,
                Role = "User",
                Remark = Remark
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Login", new { msg = "registered" });
        }
    }
}
