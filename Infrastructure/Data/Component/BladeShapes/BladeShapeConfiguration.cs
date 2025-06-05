using Domain.Component.BladeShapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.BladeShapes;

public class BladeShapeConfiguration : IEntityTypeConfiguration<BladeShape>
{
    public void Configure(EntityTypeBuilder<BladeShape> entity)
    {
        entity.Property(bs => bs.IsActive).HasDefaultValue(true);
        entity.Property(bs => bs.Price).HasDefaultValue(0);

        entity.OwnsOne(shape => shape.Name, name =>
        {
            name.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });
    }
}