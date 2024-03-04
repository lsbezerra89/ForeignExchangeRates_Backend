using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Domain.Interfaces.Repositories;
using AV.ForeignExchangeRates.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AV.ForeignExchangeRates.Infra.Repositories;

public class CurrencyExchangeRateRepository : ICurrencyExchangeRateRepository
{
    private readonly ApplicationDbContext _context;

    public CurrencyExchangeRateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public CurrencyExchangeRate? Get(string fromCurrencyCode, string toCurrencyCode)
    {
        return _context.CurrencyExchangeRates
            .Include(p => p.FromCurrency)
            .Include(p => p.ToCurrency)
            .FirstOrDefault(c => c.FromCurrency.Code == fromCurrencyCode && c.ToCurrency.Code == toCurrencyCode);
    }

    public IEnumerable<CurrencyExchangeRate> GetAllFilterByCurrency(string currencyCode)
    {
        return _context.CurrencyExchangeRates.Where(p => p.FromCurrency.Code == currencyCode || p.ToCurrency.Code == currencyCode);
    }

    public void Delete(CurrencyExchangeRate entity)
    {
        _context.CurrencyExchangeRates.Remove(entity);

        _context.SaveChanges();
    }

    public CurrencyExchangeRate Save(CurrencyExchangeRate entity)
    {
        _context.CurrencyExchangeRates.Add(entity);

        _context.SaveChanges();

        return entity;
    }
}
