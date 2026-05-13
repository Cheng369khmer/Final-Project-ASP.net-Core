using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;

namespace MyPortfolio.Pages
{
    public class AboutModel : BasePageModel
    {
        public IActionResult OnGet()
        {
            return CheckLogin() ?? Page();
        }
    }
}