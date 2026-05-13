using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyPortfolio.Models
{
    public class BasePageModel : PageModel
    {
        protected IActionResult? CheckLogin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                return RedirectToPage("/Login");
            return null;
        }
    }
}