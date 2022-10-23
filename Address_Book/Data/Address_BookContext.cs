using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Address_Book.Model;

namespace Address_Book.Data
{
    public class Address_BookContext : DbContext
    {
        public Address_BookContext (DbContextOptions<Address_BookContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; } = default!;
    }
}
