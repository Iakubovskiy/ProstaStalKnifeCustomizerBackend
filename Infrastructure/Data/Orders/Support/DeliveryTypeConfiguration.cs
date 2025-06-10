using Domain.Order.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data.Orders.Support;

public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
{
    public void Configure(EntityTypeBuilder<DeliveryType> builder)
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

        builder.OwnsOne(e => e.Comment, c =>
        {
            c.Property(t => t.TranslationDictionary)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new()
                )
                .HasColumnType("jsonb");
        });
    }
}
