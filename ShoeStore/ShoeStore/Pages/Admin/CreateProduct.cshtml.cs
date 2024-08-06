using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore.Pages.Admin
{
    public class CreateProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public SelectList Categories { get; set; } // Đảm bảo thuộc tính Categories tồn tại

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Index");
        }
    }
}
