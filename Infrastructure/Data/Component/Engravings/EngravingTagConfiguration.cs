using Domain.Component.Engravings.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Engravings;

public class EngravingTagConfiguration : IEntityTypeConfiguration<EngravingTag>
{
    public void Configure(EntityTypeBuilder<EngravingTag> builder)
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
    }
}
