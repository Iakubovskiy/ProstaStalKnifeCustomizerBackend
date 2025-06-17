using Domain.Component.Engravings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Engravings;

public class EngravingConfiguration : IEntityTypeConfiguration<Engraving>
{
    public void Configure(EntityTypeBuilder<Engraving> builder)
    {
        builder.OwnsOne(e => e.Name, name =>
        {
            name.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });

        builder.OwnsOne(e => e.Description, description =>
        {
            description.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });
        
        builder.HasMany(e => e.Tags)
            .WithMany() 
            .UsingEntity(j => j.ToTable("Engraving_EngravingTag"));
    }
}
