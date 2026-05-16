using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin
{
    public class AddUserModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddUserModel(AppDbContext context)
        {
            _context = context;
        }

        // អថេរសម្រាប់ទទួលទិន្នន័យពី Form
        [BindProperty]
        public UserModel NewUser { get; set; } = new UserModel();

        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            // ១. ឆែកមើលសុវត្ថិភាព តើមានសិទ្ធិជា Admin ដែរឬទេ?
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ឆែកសុវត្ថិភាពម្តងទៀតពេលចុចប៊ូតុង Save
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            if (!ModelState.IsValid) 
                return Page();

            try
            {
                // ២. បញ្ចូលទិន្នន័យទៅក្នុង Database
                _context.Users.Add(NewUser);
                await _context.SaveChangesAsync();
                
                // ៣. បញ្ចូលជោគជ័យ ត្រឡប់ទៅទំព័រតារាង User វិញ
                return RedirectToPage("/Admin/Users");
            }
            catch (Exception ex)
            {
                // បើមានបញ្ហា (ឧទាហរណ៍ អ៊ីមែលជាន់គ្នា) វានឹងលោត Error
                ErrorMessage = "Error adding user. Email might already exist.";
                return Page();
            }
        }
    }
}