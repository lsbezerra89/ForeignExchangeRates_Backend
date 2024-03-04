using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AV.ForeignExchangeRates.Infra.Configuration;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CurrencyMapping());
        modelBuilder.ApplyConfiguration(new CurrencyExchangeRateMapping());
    }
}
