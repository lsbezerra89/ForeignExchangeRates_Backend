namespace AV.ForeignExchangeRates.Application.Models.Request;

public class ExchangeRateRequest
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
}
