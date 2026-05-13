using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _db;
        public DashboardModel(AppDbContext db) { _db = db; }

        public string AdminName { get; set; } = string.Empty;
        public List<UserModel> Users { get; set; } = new();
        public int TotalUsers { get; set; }
        public int AdminCount { get; set; }
        public int UserCount { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin") return RedirectToPage("/Login");

            AdminName = HttpContext.Session.GetString("UserName") ?? "Admin";
            Users = _db.Users.OrderBy(u => u.ID).ToList();
            TotalUsers = Users.Count;
            AdminCount = Users.Count(u => u.Role == "Admin");
            UserCount = Users.Count(u => u.Role == "User");
            MaleCount = Users.Count(u => u.Gender == "Male");
            FemaleCount = Users.Count(u => u.Gender == "Female");

            return Page();
        }
    }
}
