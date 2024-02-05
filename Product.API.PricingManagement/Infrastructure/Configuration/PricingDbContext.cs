using Microsoft.EntityFrameworkCore;
using Product.API.PricingManagement.Infrastructure.Entities;
using System.Data.Common;

namespace Product.API.PricingManagement.Infrastructure.Configuration
{
    public class PricingDbContext:DbContext
    {
        public PricingDbContext(DbContextOptions<PricingDbContext> options) :base(options)
        {

        }

        public DbSet<PriceEntity> Prices { get; set; }
        public DbSet<CouponEntity> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceEntity>()
                .Property(x => x.Price)
                .HasColumnType("decimal(10:2)");

            modelBuilder.Entity<PriceEntity>()
                .Property(x => x.DiscountPercentage)
                .HasColumnType("decimal(5:2)");

            modelBuilder.Entity<CouponEntity>()
                .Property(x => x.Amount)
                .HasColumnType("decimal(10:2)");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Product.PricingManagement;Integrated Security=True; MultipleActiveResultSets=True; Trust Server Certificate=True");
            }
        }
    }
}
