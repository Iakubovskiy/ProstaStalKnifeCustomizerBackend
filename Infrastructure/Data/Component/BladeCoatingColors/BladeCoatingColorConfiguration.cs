using Domain.Component.BladeCoatingColors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.BladeCoatingColors;

public class BladeCoatingColorConfiguration: IEntityTypeConfiguration<BladeCoatingColor>
{
    public void Configure(EntityTypeBuilder<BladeCoatingColor> entity)
    {
        entity.Property(bc => bc.IsActive).HasDefaultValue(true);
        entity.Property(bc => bc.Price).HasDefaultValue(0);

        entity.OwnsOne(e => e.Type, type =>
        {
            type.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });

        entity.OwnsOne(e => e.Color, color =>
        {
            color.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });
    }
}