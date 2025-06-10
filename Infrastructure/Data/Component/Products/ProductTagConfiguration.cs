using Domain.Component.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Products;

public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.OwnsOne(e => e.Tag, name =>
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