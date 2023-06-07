using Microsoft.EntityFrameworkCore;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Data
{
    public class ContactApiDBContext : DbContext
    {
        public ContactApiDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
