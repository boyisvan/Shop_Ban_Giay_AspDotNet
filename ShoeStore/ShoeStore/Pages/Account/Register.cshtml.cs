using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeStore.Data;
using ShoeStore.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ShoeStore.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống.")]
            public string Username { get; set; } = null!;

            [Required(ErrorMessage = "Mật khẩu không được bỏ trống.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            [Required(ErrorMessage = "Email không được bỏ trống.")]
            [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
            public string Email { get; set; } = null!;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tạo đối tượng User mà không mã hóa mật khẩu
            var user = new User
            {
                Username = Input.Username,
                PasswordHash = Input.Password, // Không mã hóa mật khẩu
                Email = Input.Email,
                Role = "User" // Hoặc vai trò mặc định của bạn
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account/Login");
        }
    }
}
