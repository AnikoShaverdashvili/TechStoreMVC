using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Models;

namespace TechStoreMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
