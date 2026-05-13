using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly AppDbContext _db;
        public UsersModel(AppDbContext db) { _db = db; }

        public List<UserModel> Users { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            Users = _db.Users.OrderBy(u => u.ID).ToList();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                Message = $"User '{user.Name}' has been deleted.";
            }

            Users = _db.Users.OrderBy(u => u.ID).ToList();
            return Page();
        }
    }
}
