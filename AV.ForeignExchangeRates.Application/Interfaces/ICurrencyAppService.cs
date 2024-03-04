using AV.ForeignExchangeRates.Application.Models.Request;
using AV.ForeignExchangeRates.Application.Models.Response;

namespace AV.ForeignExchangeRates.Application.Interfaces;

public interface ICurrencyAppService
{
    Task<GetExchangeRateResponse> GetExchangeRate(ExchangeRateRequest request);
    CurrencyResponse GetCurrency(CurrencyRequest request);
    CurrencyResponse CreateCurrency(CreateCurrencyRequest request);
    CurrencyResponse UpdateCurrencyName(UpdateNameRequest request);
    string DeleteCurrency(CurrencyRequest request);
}
