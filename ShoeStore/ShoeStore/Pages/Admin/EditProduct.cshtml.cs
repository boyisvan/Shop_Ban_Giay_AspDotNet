using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore.Pages.Admin
{
    public class EditProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public SelectList Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", Product.CategoryId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", Product.CategoryId);
                return Page();
            }

            var productToUpdate = await _context.Products.FindAsync(Product.ProductId);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính
            productToUpdate.Name = Product.Name;
            productToUpdate.Description = Product.Description;
            productToUpdate.Price = Product.Price;
            productToUpdate.StockQuantity = Product.StockQuantity;
            productToUpdate.CategoryId = Product.CategoryId;
            productToUpdate.ImageUrl = Product.ImageUrl;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Admin/Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
