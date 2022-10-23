using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Address_Book.Data;
using Address_Book.Model;
using System.Text.RegularExpressions;

namespace Address_Book.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly Address_BookContext _context;

        public CreateModel(Address_BookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact? Contact { get; set; }

        private bool IsValid()
        {
            var contact = _context.Contact.FirstOrDefault(x => x.Phone == Contact.Phone || x.Email == Contact.Email);
            return contact != null;
        }
        private bool EmailValid()
        {
            string email = @"^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            return Regex.IsMatch(Contact.Email, email);

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (IsValid())
            {
                return BadRequest("Already exists");
            }
            if (EmailValid())
            {
                return BadRequest("Invalid Email");

            }

            _context.Contact.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
