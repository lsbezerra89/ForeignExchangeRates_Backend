using AV.ForeignExchangeRates.Domain.Configuration;
using AV.ForeignExchangeRates.Domain.Entities;
using AV.ForeignExchangeRates.Domain.Interfaces;
using AV.ForeignExchangeRates.Domain.Interfaces.Repositories;
using AV.ForeignExchangeRates.Services.Services;
using Microsoft.Extensions.Options;

namespace AV.ForeignExchangeRates.Tests.Services;

public class CurrencyExchangeRateServiceTests
{
    private readonly Mock<ICurrencyExchangeRateRepository> _currencyExchangeRateRepository;
    private readonly Mock<ICurrencyRepository> _currencyRepository;

    private readonly Mock<IHttpHelper> _httpHelper;
    private readonly Mock<IQueueService> _queueService;

    private readonly Mock<IOptions<AppSettings>> _appSettings;

    private readonly CurrencyExchangeRateService _service;

    public CurrencyExchangeRateServiceTests()
    {
        _currencyExchangeRateRepository = new Mock<ICurrencyExchangeRateRepository>();
        _httpHelper = new Mock<IHttpHelper>();
        _appSettings = new Mock<IOptions<AppSettings>>();
        _currencyRepository = new Mock<ICurrencyRepository>();
        _queueService = new Mock<IQueueService>();

        _service = new CurrencyExchangeRateService(_currencyExchangeRateRepository.Object, _httpHelper.Object, _appSettings.Object, _currencyRepository.Object, _queueService.Object);
    }

    [Fact]
    public async Task HasOnDatabase_Successfully()
    {
        _currencyExchangeRateRepository.Setup(f => f.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(
            new CurrencyExchangeRate(Guid.NewGuid(), null, null, 100m, 100m, 100m));

        var response = await _service.GetExchangeRate("USD", "BRL");

        Assert.NotNull(response);
    }

    [Fact]
    public async Task MissingOnDatabase_Successfully()
    {
        _httpHelper.Setup(f => f.GetAsync(It.IsAny<string>())).ReturnsAsync("{\r\n    \"Realtime Currency Exchange Rate\": {\r\n        \"1. From_Currency Code\": \"USD\",\r\n        \"2. From_Currency Name\": \"United States Dollar\",\r\n        \"3. To_Currency Code\": \"JPY\",\r\n        \"4. To_Currency Name\": \"Japanese Yen\",\r\n        \"5. Exchange Rate\": \"150.06400000\",\r\n        \"6. Last Refreshed\": \"2024-03-01 22:46:35\",\r\n        \"7. Time Zone\": \"UTC\",\r\n        \"8. Bid Price\": \"150.06160000\",\r\n        \"9. Ask Price\": \"150.06400000\"\r\n    }\r\n}");

        var response = await _service.GetExchangeRate("USD", "BRL");
    }

    [Fact]
    public async Task MissingOnDatabase_APIError()
    {
        _httpHelper.Setup(f => f.GetAsync(It.IsAny<string>())).ReturnsAsync("    \"Error Message\": \"Invalid API call. Please retry or visit the documentation (https://www.alphavantage.co/documentation/) for CURRENCY_EXCHANGE_RATE.\"\r\n");

        var response = await _service.GetExchangeRate("USD", "BRL");
    }
}
