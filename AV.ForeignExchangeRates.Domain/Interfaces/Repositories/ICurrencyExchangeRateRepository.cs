using AV.ForeignExchangeRates.Domain.Entities;

namespace AV.ForeignExchangeRates.Domain.Interfaces.Repositories;

public interface ICurrencyExchangeRateRepository
{
    CurrencyExchangeRate? Get(string fromCurrencyCode, string toCurrencyCode);
    IEnumerable<CurrencyExchangeRate> GetAllFilterByCurrency(string currencyCode);
    void Delete(CurrencyExchangeRate entity);
    CurrencyExchangeRate Save(CurrencyExchangeRate entity);
}
