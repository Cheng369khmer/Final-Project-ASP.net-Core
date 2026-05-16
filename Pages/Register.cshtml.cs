using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Models;
using MyPortfolio.Services; 
using System;
using System.Threading.Tasks;
using System.Linq;

namespace MyPortfolio.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly TelegramService _telegramService; 

        public RegisterModel(AppDbContext db, TelegramService telegramService) 
        { 
            _db = db; 
            _telegramService = telegramService;
        }

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

            // បាញ់សារទៅ Telegram បន្ទាប់ពី Save ចូល DB ជោគជ័យ
            string msg = $"🎉 <b>New Registration on CLU System!</b>\n" +
                         $"👤 Name: {Name}\n" +
                         $"📧 Email: {Email}\n" +
                         $"⚧ Gender: {Gender}\n" +
                         $"⏰ Time: {DateTime.Now:dd/MM/yyyy HH:mm}";
                         
            await _telegramService.SendMessageAsync(msg);

            return RedirectToPage("/Login", new { msg = "registered" });
        }
    }
}