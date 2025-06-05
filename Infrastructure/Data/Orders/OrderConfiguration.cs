using Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(o => o.Products)
            .WithMany()
            .UsingEntity<OrderItem>(
                i => i.HasOne(io => io.Product).WithMany(),
                i => i.HasOne(io => io.Order).WithMany()
            );
    }
}
