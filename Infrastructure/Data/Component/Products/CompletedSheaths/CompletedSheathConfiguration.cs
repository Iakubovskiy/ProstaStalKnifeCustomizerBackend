using Domain.Component.Product.CompletedSheath;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Component.Products.CompletedSheaths;

public class CompletedSheathConfiguration: IEntityTypeConfiguration<CompletedSheath>
{
    public void Configure(EntityTypeBuilder<CompletedSheath> builder)
    {
        builder.HasMany(s => s.Engravings)
            .WithMany();
        builder.HasMany(s => s.Attachments)
            .WithMany();
        builder.Property(s => s.TotalPriceInUah)
            .HasComputedColumnSql("public.get_completed_sheath_total_price(\"Id\")", stored:true);
    }
}