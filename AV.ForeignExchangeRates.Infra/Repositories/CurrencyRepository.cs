using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Domain.Interfaces.Repositories;
using AV.ForeignExchangeRates.Infra.Configuration;

namespace AV.ForeignExchangeRates.Infra.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;

    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Delete(Currency currency)
    {
        _context.Currencies.Remove(currency);

        _context.SaveChanges();
    }

    public Currency? GetByCode(string code)
    {
        return _context.Currencies.FirstOrDefault(c => c.Code == code);
    }

    public Currency? GetById(Guid id)
    {
        return _context.Currencies.FirstOrDefault(c => c.Id == id);
    }

    public Currency Save(Currency currency)
    {
        _context.Currencies.Add(currency);

        _context.SaveChanges();

        return currency;
    }

    public void Update(Currency currency)
    {
        _context.Currencies.Update(currency);

        _context.SaveChanges();
    }
}
