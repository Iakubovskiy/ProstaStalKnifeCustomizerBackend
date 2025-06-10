using Domain.Component.Sheaths;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Sheaths;

public class SheathConfiguration : IEntityTypeConfiguration<Sheath>
{
    public void Configure(EntityTypeBuilder<Sheath> builder)
    {
        builder.OwnsOne(e => e.Name, n =>
        {
            n.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });
    }
}
