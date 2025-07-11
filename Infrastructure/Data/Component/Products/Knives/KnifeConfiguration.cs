using Domain.Component.Product.Knife;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Component.Products.Knives;

public class KnifeConfiguration : IEntityTypeConfiguration<Knife>
{
    public void Configure(EntityTypeBuilder<Knife> builder)
    {
        builder.HasMany(k => k.Engravings)
            .WithMany();
        builder.HasMany(k => k.Attachments)
            .WithMany();
        builder.Property(k => k.TotalPriceInUah)
            .HasComputedColumnSql("public.get_knife_total_price(\"Id\")", stored:true);
    }
}
