using AV.ForeignExchangeRates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AV.ForeignExchangeRates.Infra.Mappings;

public class CurrencyExchangeRateMapping : IEntityTypeConfiguration<CurrencyExchangeRate>
{
    public void Configure(EntityTypeBuilder<CurrencyExchangeRate> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.FromCurrency).WithMany().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.ToCurrency).WithMany().OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.ExchangeRate).IsRequired();
        builder.Property(p => p.BidPrice).IsRequired();
        builder.Property(p => p.AskPrice).IsRequired();
    }
}
