using AV.ForeignExchangeRates.Domain.Entities;

namespace AV.ForeignExchangeRates.Domain.Interfaces.Services;

public interface ICurrencyExchangeRateService
{
    Task<CurrencyExchangeRate?> GetExchangeRate(string fromCurrencyCode, string toCurrencyCode);
}
