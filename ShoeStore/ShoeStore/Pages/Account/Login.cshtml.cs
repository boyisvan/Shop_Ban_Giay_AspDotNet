using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeStore.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Username == Input.Username && u.PasswordHash == Input.Password);
                if (user != null)
                {
                    if (user.Role == "Admin")
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    else
                    {
                        return RedirectToPage("/UserDashboard/Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc tài khoản không đúng.");
            }

            return Page();
        }
    }
}
