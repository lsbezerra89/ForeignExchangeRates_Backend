using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Domain.Interfaces.Repositories;
using AV.ForeignExchangeRates.Domain.Interfaces.Services;

namespace AV.ForeignExchangeRates.Services.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;

    public CurrencyService(ICurrencyRepository currencyRepository, ICurrencyExchangeRateRepository currencyExchangeRateRepository)
    {
        _currencyRepository = currencyRepository;
        _currencyExchangeRateRepository = currencyExchangeRateRepository;
    }

    public Currency AddCurrency(Currency currency)
    {
        _currencyRepository.Save(currency);

        return currency;
    }

    public void DeleteCurrency(string code)
    {
        var currency = _currencyRepository.GetByCode(code);

        if (currency == null) throw new Exception("Currency not found");

        var currencyExchangeRates = _currencyExchangeRateRepository.GetAllFilterByCurrency(code);

        foreach (var currencyExchangeRate in currencyExchangeRates) _currencyExchangeRateRepository.Delete(currencyExchangeRate);

        _currencyRepository.Delete(currency);
    }

    public Currency? GetCurrency(string code)
    {
        var currency = _currencyRepository.GetByCode(code);

        if (currency == null) throw new Exception("Currency not found");

        return currency;
    }

    public Currency UpdateCurrencyName(string name, string code)
    {
        var currency = _currencyRepository.GetByCode(code);

        if (currency == null) throw new Exception("Currency not found");

        currency.SetName(name);

        _currencyRepository.Update(currency);

        return currency;
    }
}
