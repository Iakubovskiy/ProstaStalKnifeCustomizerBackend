using Domain.Component.Product.Attachments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Component.Products.Attachments;

public class AttachmentTypeConfiguration : IEntityTypeConfiguration<AttachmentType>
{
    public void Configure(EntityTypeBuilder<AttachmentType> builder)
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