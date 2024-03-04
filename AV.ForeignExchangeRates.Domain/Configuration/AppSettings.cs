namespace AV.ForeignExchangeRates.Domain.Configuration;

public class AppSettings
{
    public string AlphavantageApiUrl { get; set; }
    public object AlphavantageApiKey { get; set; }
    public string QueueConnectionString { get; set; }
}
