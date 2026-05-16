using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Data;
using MyPortfolio.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace MyPortfolio.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env; // ប្រើសម្រាប់ទាញទីតាំងរក្សាទុករូបភាព

        public EditUserModel(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public UserModel EditUser { get; set; } = default!;

        // សម្រាប់ទទួលរូបថតថ្មីពី Form បើគេចង់ប្តូររូប
        [BindProperty]
        public IFormFile? PhotoUpload { get; set; } 

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            if (id == null) return RedirectToPage("/Admin/Users");

            var user = await _context.Users.FindAsync(id);
            if (user == null) return RedirectToPage("/Admin/Users");

            EditUser = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToPage("/Login");

            if (!ModelState.IsValid) return Page();

            var userToUpdate = await _context.Users.FindAsync(EditUser.ID);
            
            if (userToUpdate == null) return RedirectToPage("/Admin/Users");

            // 🛑 ការពារសុវត្ថិភាព៖ បើគណនីដើមជា Admin ហាមប្តូរតួនាទីទៅជា User វិញដាច់ខាត!
            if (userToUpdate.Role == "Admin" && EditUser.Role != "Admin")
            {
                ErrorMessage = "⚠️ Security Warning: Cannot change the role of an Administrator account!";
                return Page();
            }

            // កែប្រែទិន្នន័យទូទៅ
            userToUpdate.Name = EditUser.Name;
            userToUpdate.Email = EditUser.Email;
            userToUpdate.Gender = EditUser.Gender;
            userToUpdate.Role = EditUser.Role;
            userToUpdate.Remark = EditUser.Remark;
            
            // 👉 បន្ថែមការកែប្រែស្ថានភាព Active/Inactive
            userToUpdate.IsActive = EditUser.IsActive;

            // ប្រសិនបើមានការកែប្រែលេចសម្ងាត់ថ្មី
            if (!string.IsNullOrEmpty(EditUser.Password))
            {
                userToUpdate.Password = EditUser.Password;
            }

            // 👉 ប្រសិនបើមានគេ Upload រូបថតថ្មី យើងធ្វើការ Save វា
            if (PhotoUpload != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "avatars");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoUpload.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PhotoUpload.CopyToAsync(fileStream);
                }
                
                // កត់ត្រាទីតាំងរូបភាពថ្មីចូល Database
                userToUpdate.ProfilePhoto = "/images/avatars/" + uniqueFileName;
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Admin/Users");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error updating user: " + ex.Message;
                return Page();
            }
        }
    }
}