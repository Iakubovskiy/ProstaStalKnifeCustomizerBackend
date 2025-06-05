using Domain.Component.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.IsActive)
               .HasDefaultValue(true);

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });

        builder.OwnsOne(p => p.Description, d =>
        {
            d.Property(t => t.TranslationDictionary)
             .HasConversion(
                 v => JsonConvert.SerializeObject(v),
                 v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
             )
             .HasColumnType("jsonb");
        });

        builder.OwnsOne(p => p.Title, t =>
        {
            t.Property(t => t.TranslationDictionary)
             .HasConversion(
                 v => JsonConvert.SerializeObject(v),
                 v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
             )
             .HasColumnType("jsonb");
        });

        builder.OwnsOne(p => p.MetaTitle, mt =>
        {
            mt.Property(t => t.TranslationDictionary)
              .HasConversion(
                  v => JsonConvert.SerializeObject(v),
                  v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
              )
              .HasColumnType("jsonb");
        });

        builder.OwnsOne(p => p.MetaDescription, md =>
        {
            md.Property(t => t.TranslationDictionary)
              .HasConversion(
                  v => JsonConvert.SerializeObject(v),
                  v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
              )
              .HasColumnType("jsonb");
        });
    }
}
