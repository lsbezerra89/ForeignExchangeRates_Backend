using AV.ForeignExchangeRates.Domain.Interfaces;

namespace AV.ForeignExchangeRates.Services.Http;

public class HttpHelper : IHttpHelper, IDisposable
{    
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpHelper(IHttpClientFactory httpClientFactory)
    {        
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetAsync(string url)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }

    public void Dispose()
    {
        //httpClient.Dispose();
    }
}
