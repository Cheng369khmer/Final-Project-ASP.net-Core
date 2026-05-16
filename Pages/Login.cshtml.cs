using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly TelegramService _telegramService;

        public LoginModel(AppDbContext db, TelegramService telegramService)
        {
            _db = db;
            _telegramService = telegramService;
        }

        public string ErrorMessage { get; set; } = string.Empty;
        public string InfoMessage { get; set; } = string.Empty;

        // កូដតេស្ត៖ រាល់ពេល Refresh ទំព័រ Login វានឹងបាញ់សារទៅ Telegram ភ្លាម
        public async Task OnGetAsync(string? msg)
        {
            if (msg == "registered") InfoMessage = "Account created! Please login.";

            // បាញ់សារតេស្តទៅ Telegram
            await _telegramService.SendMessageAsync("🔔 Test Message: C# Application របស់អ្នកកំពុងដំណើរការ!");
        }

        public async Task<IActionResult> OnPostAsync(string Email, string Password)
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

            string telegramMsg = $"✅ <b>User Logged In</b>\n" +
                                 $"👤 Name: {user.Name}\n" +
                                 $"📧 Email: {user.Email}\n" +
                                 $"🔑 Role: {user.Role}\n" +
                                 $"⏰ Time: {DateTime.Now:dd/MM/yyyy HH:mm}";
                                 
            await _telegramService.SendMessageAsync(telegramMsg);

            if (user.Role == "Admin")
            {
                return RedirectToPage("/Admin/Dashboard");
            }

            return RedirectToPage("/Index");
        }
    }
}