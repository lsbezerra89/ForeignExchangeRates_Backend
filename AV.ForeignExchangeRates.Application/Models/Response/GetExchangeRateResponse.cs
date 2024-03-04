using Newtonsoft.Json;

namespace AV.ForeignExchangeRates.Application.Models.Response;

public class GetExchangeRateResponse
{
    [JsonProperty("1. From_Currency Code")]
    public string FromCurrencyCode { get; set; }

    [JsonProperty("2. From_Currency Name")]
    public string FromCurrencyName { get; set; }

    [JsonProperty("3. To_Currency Code")]
    public string ToCurrencyCode { get; set; }

    [JsonProperty("4. To_Currency Name")]
    public string ToCurrencyName { get; set; }

    [JsonProperty("5. Exchange Rate")]
    public decimal ExchangeRate { get; set; }

    [JsonProperty("8. Bid Price")]
    public decimal BidPrice { get; set; }

    [JsonProperty("9. Ask Price")]
    public decimal AskPrice { get; set; }
}
