using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<MsCustomer> MsCustomer { get; set; }
        public DbSet<LtPayments> LtPayment { get; set; }
        public DbSet<TrRental> TrRental { get; set; }
        public DbSet<MsCar> Mscar { get; set; }
        public DbSet<MsCarImages> MsCarImages { get; set; }
        public DbSet<TrMaintenance> TrMaintenance { get; set; }
        public DbSet<MsEmployee> MsEmployee { get; set; }
    }
}
