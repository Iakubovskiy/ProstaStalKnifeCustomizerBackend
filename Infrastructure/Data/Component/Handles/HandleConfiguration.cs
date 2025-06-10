using Domain.Component.Handles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Handles;

public class HandleConfiguration : IEntityTypeConfiguration<Handle>
{
    public void Configure(EntityTypeBuilder<Handle> entity)
    {
        entity.Property(h => h.IsActive).HasDefaultValue(true);

        entity.OwnsOne(handle => handle.Color, color =>
        {
            color.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });

        entity.OwnsOne(handle => handle.Material, material =>
        {
            material.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new())
                .HasColumnType("jsonb");
        });
    }
}