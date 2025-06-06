using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Engravings;
using Domain.Component.Engravings.Support;
using Domain.Component.Handles;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Currencies;
using Domain.Order;
using Domain.Order.Support;
using Domain.Users;
using Infrastructure.Data.Component.BladeCoatingColors;
using Infrastructure.Data.Component.BladeShapes;
using Infrastructure.Data.Component.Engravings;
using Infrastructure.Data.Component.Engravings.EngravingTags;
using Infrastructure.Data.Component.Handles;
using Infrastructure.Data.Component.Products;
using Infrastructure.Data.Component.Products.Attachments;
using Infrastructure.Data.Component.Products.Knives;
using Infrastructure.Data.Component.Sheaths;
using Infrastructure.Data.Component.Sheaths.Color;
using Infrastructure.Data.Orders;
using Infrastructure.Data.Orders.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }
        public override DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BladeCoatingColor> BladeCoatingColors { get; set; }
        public DbSet<BladeShape> BladeShapes { get; set; }
        public DbSet<BladeShapeType> BladeShapeTypes { get; set; }
        public DbSet<Engraving> Engravings { get; set; }
        public DbSet<EngravingPrice> EngravingPrices { get; set; }
        public DbSet<EngravingTag> EngravingTags { get; set; }
        public DbSet<Handle> Handles { get; set; }
        public DbSet<Sheath> Sheaths { get; set; }
        public DbSet<SheathColor> SheathColors { get; set; }
        public DbSet<Texture> Textures { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CompletedSheath> CompletedSheaths { get; set; }
        
        public DbSet<Knife> Knives { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BladeCoatingColorConfiguration());
            modelBuilder.ApplyConfiguration(new BladeShapeConfiguration());
            modelBuilder.ApplyConfiguration(new HandleConfiguration());
            modelBuilder.ApplyConfiguration(new SheathColorConfiguration());
            modelBuilder.ApplyConfiguration(new SheathColorPriceByTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EngravingConfiguration());
            modelBuilder.ApplyConfiguration(new EngravingTagConfiguration());
            modelBuilder.ApplyConfiguration(new KnifeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SheathConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
        }
    }
}
