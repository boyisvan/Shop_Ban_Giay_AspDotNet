using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Threading.Tasks;

namespace ShoeStore.Pages.Admin.Customers
{
    public class EditCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _context.Customers.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customerToUpdate = await _context.Customers.FindAsync(Customer.CustomerId);

            if (customerToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính
            customerToUpdate.FirstName = Customer.FirstName;
            customerToUpdate.LastName = Customer.LastName;
            customerToUpdate.Email = Customer.Email;
            customerToUpdate.PhoneNumber = Customer.PhoneNumber;
            customerToUpdate.Address = Customer.Address;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
