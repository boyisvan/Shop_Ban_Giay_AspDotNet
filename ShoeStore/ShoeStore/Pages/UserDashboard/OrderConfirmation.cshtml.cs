using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using System;
using System.Threading.Tasks;

namespace ShoeStore.Pages.UserDashboard
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderConfirmationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public decimal TotalAmount { get; set; }
        [BindProperty]
        public int CustomerId { get; set; }
        [BindProperty]
        public int ProductId { get; set; }

        public async Task<IActionResult> OnGetAsync(int customerId, int productId)
        {
            CustomerId = customerId;
            ProductId = productId;

            Product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == ProductId);

            if (Product == null)
            {
                return NotFound();
            }

            TotalAmount = Product.Price;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var customer = await _context.Customers.FindAsync(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customer.CustomerId,
                TotalAmount = product.Price,
                OrderStatus = "Pending"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Hiển thị thông báo thành công và chuyển đến trang khác nếu cần
            TempData["SuccessMessage"] = "Đơn hàng của bạn đã được xác nhận!";
            return RedirectToPage("/UserDashboard/Success");
        }
    }
}
