using AV.ForeignExchangeRates.Application.Interfaces;
using AV.ForeignExchangeRates.Application.Models.Request;
using AV.ForeignExchangeRates.Application.Models.Response;
using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Domain.Interfaces.Services;

namespace AV.ForeignExchangeRates.Application.AppServices;

public class CurrencyAppService : ICurrencyAppService
{
    private readonly ICurrencyExchangeRateService _currencyExchangeRateService;
    private readonly ICurrencyService _currencyService;

    public CurrencyAppService(ICurrencyExchangeRateService currencyExchangeRateService, ICurrencyService currencyService)
    {
        _currencyExchangeRateService = currencyExchangeRateService;
        _currencyService = currencyService;
    }

    public async Task<GetExchangeRateResponse> GetExchangeRate(ExchangeRateRequest request)
    {
        var exchangeRateService = await _currencyExchangeRateService.GetExchangeRate(request.FromCurrency, request.ToCurrency);

        if (exchangeRateService is null) throw new Exception("Error");

        var response = new GetExchangeRateResponse()
        {
            FromCurrencyCode = exchangeRateService.FromCurrency.Code,
            FromCurrencyName = exchangeRateService.FromCurrency.Name,
            ToCurrencyCode = exchangeRateService.ToCurrency.Code,
            ToCurrencyName = exchangeRateService.ToCurrency.Name,
            ExchangeRate = exchangeRateService.ExchangeRate,
            BidPrice = exchangeRateService.BidPrice,
            AskPrice = exchangeRateService.AskPrice
        };

        return response;
    }

    public CurrencyResponse CreateCurrency(CreateCurrencyRequest request)
    {
        var currency = _currencyService.AddCurrency(new Currency(Guid.NewGuid(), request.Code, request.Name));

        return new CurrencyResponse()
        {
            Id = currency.Id,
            Code = currency.Code,
            Name = currency.Name,
        };
    }

    public string DeleteCurrency(CurrencyRequest request)
    {
        _currencyService.DeleteCurrency(request.Code);

        return $"Currency {request.Code} successfully deleted!";
    }

    public CurrencyResponse GetCurrency(CurrencyRequest request)
    {
        var currency = _currencyService.GetCurrency(request.Code);

        return new CurrencyResponse()
        {
            Id = currency.Id,
            Code = currency.Code,
            Name = currency.Name,
        };
    }

    public CurrencyResponse UpdateCurrencyName(UpdateNameRequest request)
    {
        var currency = _currencyService.UpdateCurrencyName(request.Name, request.Code);

        return new CurrencyResponse()
        {
            Id = currency.Id,
            Code = currency.Code,
            Name = currency.Name,
        };
    }
}
