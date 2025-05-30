using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Engravings;
using Domain.Component.Engravings.Support;
using Domain.Component.Handles;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Order;
using Domain.Order.Support;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;

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
        
        public DbSet<Knife> Knives { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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

            modelBuilder.Entity<Handle>(entity => {
                entity.Property(hc => hc.IsActive).HasDefaultValue(true);
            });
            
            modelBuilder.Entity<SheathColorPriceByType>()
                .HasKey(p => new { p.TypeId, p.SheathColorId });
            
            modelBuilder.Entity<SheathColorPriceByType>()
                .HasOne(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId);
        
            modelBuilder.Entity<SheathColorPriceByType>()
                .HasOne(p => p.SheathColor)
                .WithMany(s => s.Prices)
                .HasForeignKey(p => p.SheathColorId);

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

            #region Translations

            modelBuilder.Entity<BladeCoatingColor>(entity =>
            {
                entity.OwnsOne(e => e.Type, type =>
                {
                    type.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });

                entity.OwnsOne(e => e.Color, color =>
                {
                    color.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<BladeShape>(entity =>
            {
                entity.OwnsOne(shape => shape.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<Handle>(entity =>
            {
                entity.OwnsOne(handle => handle.Color, color =>
                {
                    color.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });

                entity.OwnsOne(handle => handle.Material, material =>
                {
                    material.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<Sheath>(entity =>
            {
                entity.OwnsOne(sheath => sheath.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<SheathColor>(entity =>
            {
                entity.OwnsOne(sheathColor => sheathColor.Color, color =>
                {
                    color.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                entity.OwnsOne(sheathColor => sheathColor.Material, color =>
                {
                    color.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<EngravingTag>(entity =>
            {
                entity.OwnsOne(tag => tag.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<Engraving>(entity =>
            {
                entity.OwnsOne(engraving => engraving.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(engraving => engraving.Description, description =>
                {
                    description.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<Product>(entity =>
            {
                entity.OwnsOne(product => product.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(product => product.Description, description =>
                {
                    description.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(product => product.Title, title =>
                {
                    title.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(product => product.MetaDescription, metaDescription =>
                {
                    metaDescription.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(product => product.MetaTitle, meatTitle =>
                {
                    meatTitle.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.OwnsOne(attachment => attachment.Color, color =>
                {
                    color.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(attachment => attachment.Material, material =>
                {
                    material.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<AttachmentType>(entity =>
            {
                entity.OwnsOne(type => type.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.OwnsOne(deliveryType => deliveryType.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(deliveryType => deliveryType.Comment, comment =>
                {
                    comment.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });
            
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.OwnsOne(paymentMethod => paymentMethod.Name, name =>
                {
                    name.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
                
                entity.OwnsOne(paymentMethod => paymentMethod.Description, description =>
                {
                    description.Property(t => t.TranslationDictionary)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) 
                                 ?? new Dictionary<string, string>()
                        )
                        .HasColumnType("jsonb");
                });
            });

            #endregion
        }
    }
}
