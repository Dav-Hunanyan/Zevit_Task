using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Address_Book.Data;
using Address_Book.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Address_Book.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly Address_BookContext _context;

        public IndexModel(Address_BookContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList FullName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ContactName { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Contact
                                            orderby m.FullName
                                            select m.FullName;

            var contacts = from m in _context.Contact
                           select m;

            if (!string.IsNullOrEmpty(ContactName))
            {
                contacts = contacts.Where(x => x.FullName == ContactName);
            }


            if (!string.IsNullOrEmpty(SearchString))
            {
                contacts = contacts.Where(s => s.FullName.Contains(SearchString));
            }
            FullName = new SelectList(await genreQuery.Distinct().ToListAsync());
            Contact = await contacts.ToListAsync();
        }
    }
}
