using Domain.Component.Product.Attachments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Products.Attachments;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.OwnsOne(e => e.Color, c =>
        {
            c.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });

        builder.OwnsOne(e => e.Material, m =>
        {
            m.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });
    }
}
