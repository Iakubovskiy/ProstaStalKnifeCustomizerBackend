using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkshopBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkshopBackend.Data
{
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BladeCoatingColor> BladeCoatingColors { get; set; }
        public DbSet<BladeShape> BladeShapes { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<Engraving> Engravings { get; set; }
        public DbSet<EngravingPrice> EngravingPrices { get; set; }
        public DbSet<Fastening> Fastenings { get; set; }
        public DbSet<HandleColor> HandleColors { get; set; }
        public DbSet<Knife> Knives { get; set; }
        public DbSet<OrderStatuses> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SheathColor> SheathColors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BladeCoatingColor>(entity => { 
                entity.Property(bc => bc.IsActive).HasDefaultValue(true);
                entity.Property(bc => bc.Price).HasDefaultValue(0);
            });

            modelBuilder.Entity<BladeShape>(entity => {
                entity.Property(bs => bs.IsActive).HasDefaultValue(true);
                entity.Property(bs => bs.Price).HasDefaultValue(0);
            });

            modelBuilder.Entity<HandleColor>(entity => {
                entity.Property(hc => hc.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<SheathColor>(entity => {
                entity.Property(sc => sc.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Product>().Property(p=>p.IsActive).HasDefaultValue(true);

            modelBuilder.Entity<Knife>()
                .HasMany(k => k.Engravings)
                .WithMany();

            modelBuilder.Entity<Order>()
                .HasMany(p => p.Products)
                .WithMany()
                .UsingEntity<OrderItem>(
                    i => i.HasOne(io => io.Product).WithMany(),
                    i => i.HasOne(io => io.Order).WithMany()
                );
        }
    }
}
