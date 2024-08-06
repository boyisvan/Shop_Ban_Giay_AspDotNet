using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Threading.Tasks;

namespace ShoeStore.Pages.UserDashboard
{
    public class CustomerInfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomerInfoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new Customer();

        [BindProperty]
        public int ProductId { get; set; }

        public async Task<IActionResult> OnGetAsync(int productId)
        {
            ProductId = productId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Lưu thông tin khách hàng vào cơ sở dữ liệu
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            // Chuyển đến trang xác nhận đơn hàng
            return RedirectToPage("/UserDashboard/OrderConfirmation", new { customerId = Customer.CustomerId, productId = ProductId });
        }
    }
}
