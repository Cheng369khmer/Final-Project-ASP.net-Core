using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;

namespace MyPortfolio.Pages
{
    public class PortfolioModel : BasePageModel
    {
        public IActionResult OnGet()
        {
            return CheckLogin() ?? Page();
        }
    }
}