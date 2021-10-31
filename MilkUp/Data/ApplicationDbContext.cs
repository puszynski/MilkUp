using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilkUp.Models;

namespace MilkUp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyPayment> CompanyPayments { get; set; }
        public DbSet<Cow> Cows { get; set; }
        public DbSet<CowGroup> CowGroups { get; set; }
        public DbSet<Farm> Farms {  get; set; }
        public DbSet<Lactation> Lactations {  get; set; }
        public DbSet<Bull> Bulls { get; set; } 
    }
}