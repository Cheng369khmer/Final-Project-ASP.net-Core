using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly AppDbContext _context;

        public UsersModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<UserModel> Users { get; set; } = new List<UserModel>();

        // អថេរសម្រាប់ទុកពាក្យដែលគេ Search
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            // ១. ឆែកមើលសុវត្ថិភាព តើមានសិទ្ធិជា Admin ដែរឬទេ?
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            // ២. រៀបចំទាញទិន្នន័យទាំងអស់ (តម្រៀបតាម ID)
            var query = _context.Users.OrderBy(u => u.ID).AsQueryable();

            // ៣. បើមានគេវាយអក្សរ Search យើងធ្វើការចម្រាញ់ (Filter)
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u => u.Name.Contains(SearchTerm) || u.Email.Contains(SearchTerm));
            }

            // ៤. បញ្ជូនទិន្នន័យទៅឱ្យ UI
            Users = await query.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // ១. ឆែកមើលសុវត្ថិភាពមុនពេលអនុញ្ញាតឱ្យលុប
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            // ២. ស្វែងរកទិន្នន័យដែលត្រូវលុប
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                Message = $"User '{user.Name}' has been deleted.";
            }

            // ៣. បន្ទាប់ពីលុបហើយ ត្រូវទាញទិន្នន័យមកបង្ហាញម្តងទៀត (រក្សាការ Search ទុកដដែលបើសិនជាមាន)
            var query = _context.Users.OrderBy(u => u.ID).AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u => u.Name.Contains(SearchTerm) || u.Email.Contains(SearchTerm));
            }

            Users = await query.ToListAsync();

            return Page();
        }
    }
}