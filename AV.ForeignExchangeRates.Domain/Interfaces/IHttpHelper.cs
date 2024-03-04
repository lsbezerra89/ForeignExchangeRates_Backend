namespace AV.ForeignExchangeRates.Domain.Interfaces;

public interface IHttpHelper
{
    Task<string> GetAsync(string url);
}
