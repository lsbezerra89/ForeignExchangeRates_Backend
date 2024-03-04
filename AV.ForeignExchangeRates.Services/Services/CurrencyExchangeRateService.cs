using AV.ForeignExchangeRates.Domain.Configuration;
using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Domain.Interfaces;
using AV.ForeignExchangeRates.Domain.Interfaces.Repositories;
using AV.ForeignExchangeRates.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AV.ForeignExchangeRates.Services.Services;

public class CurrencyExchangeRateService : ICurrencyExchangeRateService
{
    private readonly ICurrencyExchangeRateRepository _currencyExchangeRateRepository;
    private readonly ICurrencyRepository _currencyRepository;

    private readonly IHttpHelper _httpHelper;
    private readonly IQueueService _queueService;

    private readonly AppSettings _appSettings;

    public CurrencyExchangeRateService(ICurrencyExchangeRateRepository currencyExchangeRateRepository, IHttpHelper httpHelper, IOptions<AppSettings> appSettings, ICurrencyRepository currencyRepository, IQueueService queueService)
    {
        _currencyExchangeRateRepository = currencyExchangeRateRepository;
        _httpHelper = httpHelper;
        _appSettings = appSettings.Value;
        _currencyRepository = currencyRepository;
        _queueService = queueService;
    }

    public async Task<CurrencyExchangeRate?> GetExchangeRate(string fromCurrencyCode, string toCurrencyCode)
    {
        var currency = _currencyExchangeRateRepository.Get(fromCurrencyCode, toCurrencyCode);

        if(currency is null)
        {
            try
            {
                var response = await _httpHelper.GetAsync($"{_appSettings.AlphavantageApiUrl}query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromCurrencyCode}&to_currency={toCurrencyCode}&apikey={_appSettings.AlphavantageApiKey}");

                var baseResponse = JsonConvert.DeserializeObject<CurrencyDTO>(response);

                if (baseResponse is null) throw new Exception("API Error: " + response);

                var apiExchangeRateResponse = baseResponse.CurrentyExchangeRateDTO;

                var fromCurrency = _currencyRepository.GetByCode(fromCurrencyCode);
                if (fromCurrency is null)
                    fromCurrency = CreateNewCurrency(fromCurrencyCode, apiExchangeRateResponse.FromCurrencyName);

                var toCurrency = _currencyRepository.GetByCode(toCurrencyCode);
                if (toCurrency is null)
                    toCurrency = CreateNewCurrency(toCurrencyCode, apiExchangeRateResponse.ToCurrencyName);

                currency = new CurrencyExchangeRate(
                    Guid.NewGuid(),
                    toCurrency,
                    fromCurrency,
                    decimal.Parse(apiExchangeRateResponse.ExchangeRate),
                    decimal.Parse(apiExchangeRateResponse.BidPrice),
                    decimal.Parse(apiExchangeRateResponse.AskPrice));

                _currencyExchangeRateRepository.Save(currency);

                await _queueService.SendMessage("exchange", JsonConvert.SerializeObject(currency));
            }
            catch(Exception)
            {
                throw;
            }
        }

        return currency;
    }

    private Currency CreateNewCurrency(string fromCurrencyCode, string fromCurrencyName)
    {
        var newCurrency = new Currency(Guid.NewGuid(), fromCurrencyCode, fromCurrencyName);
        return _currencyRepository.Save(newCurrency);
    }

    private record CurrentyExchangeRateDTO
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
        public string ExchangeRate { get; set; }

        [JsonProperty("8. Bid Price")]
        public string BidPrice { get; set; }

        [JsonProperty("9. Ask Price")]
        public string AskPrice { get; set; }
    }

    private record CurrencyDTO
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public CurrentyExchangeRateDTO CurrentyExchangeRateDTO { get; set; }
    }

    private record ErrorDTO
    {

    }
}
