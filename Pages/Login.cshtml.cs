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

        public void OnGet(string? msg)
        {
            if (msg == "registered") InfoMessage = "Account created! Please login.";
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

            // ចាប់យក IP Address របស់អ្នកដែល Login ចូល
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            if (ipAddress == "::1") ipAddress = "127.0.0.1 (Localhost)"; // បើថតនៅលើម៉ាស៊ីនខ្លួនឯង

            // បាញ់សារទៅ Telegram រួមទាំង IP Address
            string telegramMsg = $"✅ <b>User Logged In</b>\n" +
                                 $"👤 Name: {user.Name}\n" +
                                 $"📧 Email: {user.Email}\n" +
                                 $"🔑 Role: {user.Role}\n" +
                                 $"🌐 IP Address: <code>{ipAddress}</code>\n" +
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