using Newtonsoft.Json;

namespace AV.ForeignExchangeRates.Application.Models.Request;

public class CurrencyRequest
{
    [JsonProperty("code")]
    public string Code { get; set; }
}
