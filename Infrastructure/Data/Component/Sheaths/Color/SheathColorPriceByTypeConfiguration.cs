using Domain.Component.Sheaths.Color;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Component.Sheaths.Color;

public class SheathColorPriceByTypeConfiguration : IEntityTypeConfiguration<SheathColorPriceByType>
{
    public void Configure(EntityTypeBuilder<SheathColorPriceByType> entity)
    {
        entity.HasKey(p => new { p.TypeId, p.SheathColorId });

        entity.HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId);

        entity.HasOne(p => p.SheathColor)
            .WithMany(s => s.Prices)
            .HasForeignKey(p => p.SheathColorId);
    }
}