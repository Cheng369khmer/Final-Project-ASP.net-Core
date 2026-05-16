using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Services; 
using System;
using System.Threading.Tasks;

namespace MyPortfolio.Pages
{
    // ត្រូវប្រាកដថាឈ្មោះ Class នៅទីនេះគឺ LogoutModel
    public class LogoutModel : PageModel
    {
        private readonly TelegramService _telegramService;

        public LogoutModel(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        public async Task<IActionResult> OnGet()
        {
            // បាញ់សារទៅ Telegram មុនពេល Clear Session
            string msg = $"🚪 <b>User Logged Out</b>\n" +
                         $"⏰ Time: {DateTime.Now:dd/MM/yyyy HH:mm}";
                         
            await _telegramService.SendMessageAsync(msg);

            // លុប Session ចោល
            HttpContext.Session.Clear();
            
            return RedirectToPage("/Index");
        }
    }
}