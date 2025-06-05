using Domain.Component.Sheaths.Color;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Sheaths.Color;

public class SheathColorConfiguration : IEntityTypeConfiguration<SheathColor>
{
    public void Configure(EntityTypeBuilder<SheathColor> entity)
    {
        entity.Property(sc => sc.IsActive).HasDefaultValue(true);

        entity.OwnsOne(sheathColor => sheathColor.Color, color =>
        {
            color.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });

        entity.OwnsOne(sheathColor => sheathColor.Material, color =>
        {
            color.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });
    }
}