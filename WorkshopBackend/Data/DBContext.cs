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
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Admin> Admins { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<BladeCoating> BladeCoatings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<BladeCoatingColor> BladeCoatingColors { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<BladeShape> BladeShapes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<DeliveryType> DeliveryTypes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Engraving> Engravings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<EngravingPrice> EngravingPrices { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Fastening> Fastenings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<HandleColor> HandleColors { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Knife> Knives { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrderStatuses> OrderStatuses { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<SheathColor> SheathColors { get; set; }        
    }
}
