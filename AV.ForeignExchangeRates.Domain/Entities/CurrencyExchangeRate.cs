using AV.ForeignExchangeRates.Domain.Validators;
using FluentValidation;

namespace AV.ForeignExchangeRates.Domain.Entities;

public class CurrencyExchangeRate
{
    public Guid Id { get; private set; }
    public Currency ToCurrency { get; private set; }
    public Currency FromCurrency { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public decimal BidPrice { get; private set; }
    public decimal AskPrice { get; private set; }

    protected CurrencyExchangeRate() { } // for ef

    public CurrencyExchangeRate(Guid id, Currency toCurrency, Currency fromCurrency, 
        decimal exchangeRate, decimal bidPrice, decimal askPrice)
    {
        Id = id;
        ToCurrency = toCurrency;
        FromCurrency = fromCurrency;
        ExchangeRate = exchangeRate;
        BidPrice = bidPrice;
        AskPrice = askPrice;

        new CurrencyExchangeRateValidator().ValidateAndThrow(this);
    }
}
