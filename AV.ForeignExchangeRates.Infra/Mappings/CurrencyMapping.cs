using AV.ForeignExchangeRates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AV.ForeignExchangeRates.Infra.Mappings;

public class CurrencyMapping : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Code).IsUnique();

        builder.Property(p => p.Code).IsRequired().HasMaxLength(4);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(64);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
    }
}
