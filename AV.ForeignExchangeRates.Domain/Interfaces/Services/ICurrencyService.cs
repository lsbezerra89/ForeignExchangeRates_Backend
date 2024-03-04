using AV.ForeignExchangeRates.Domain.Entities;

namespace AV.ForeignExchangeRates.Domain.Interfaces.Services;

public interface ICurrencyService
{
    Currency? GetCurrency(string code);
    Currency AddCurrency(Currency currency);
    void DeleteCurrency(string code);
    Currency UpdateCurrencyName(string name, string code);
}
