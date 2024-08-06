using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Threading.Tasks;

namespace ShoeStore.Pages.Admin.Customers
{
    public class CreateCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
