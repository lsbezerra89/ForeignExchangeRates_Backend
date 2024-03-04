using AV.ForeignExchangeRates.API.Controllers;
using AV.ForeignExchangeRates.Application.Interfaces;
using AV.ForeignExchangeRates.Application.Models.Request;
using AV.ForeignExchangeRates.Application.Models.Response;
using AV.ForeignExchangeRates.Application.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AV.ForeignExchangeRates.Tests.API;

public class CurrencyControllerTests
{
    private readonly Mock<ICurrencyAppService> _currencyAppService;
    private readonly Mock<IValidator<CurrencyRequest>> _currencyRequestValidator;
    private readonly Mock<IValidator<UpdateNameRequest>> _updateNameRequestValidator;
    private readonly Mock<IValidator<CreateCurrencyRequest>> _createCurrencyRequestValidator;

    private readonly CurrencyController _controller;

    public CurrencyControllerTests()
    {
        _currencyAppService = new Mock<ICurrencyAppService>();
        _currencyRequestValidator = new Mock<IValidator<CurrencyRequest>>();
        _updateNameRequestValidator = new Mock<IValidator<UpdateNameRequest>>();
        _createCurrencyRequestValidator = new Mock<IValidator<CreateCurrencyRequest>>();

        _controller = new CurrencyController(_currencyAppService.Object, new ExchangeRateRequestValidator(), _currencyRequestValidator.Object, _updateNameRequestValidator.Object, _createCurrencyRequestValidator.Object);
    }

    [Fact]
    public async Task GetExchangeRate_Successfully()
    {
        //Arrange
        _currencyAppService.Setup(p => p.GetExchangeRate(It.IsAny<ExchangeRateRequest>())).ReturnsAsync(new GetExchangeRateResponse()
        {
            FromCurrencyCode = "USD",
            FromCurrencyName = "United States Dollar",
            ToCurrencyCode = "JPY",
            ToCurrencyName = "Japanese Yen",
            ExchangeRate = 150.13m,
            BidPrice = 150.13m,
            AskPrice = 150.13m
        });

        //Act
        var response = await _controller.GetExchangeRate(new ExchangeRateRequest
        {
            FromCurrency = "USD",
            ToCurrency = "EUR"
        });

        //Assert
        Assert.IsType<OkObjectResult>(response);
        var okResult = (OkObjectResult)response;

        Assert.IsType<GetExchangeRateResponse>(okResult.Value);
        var responseParsed = (GetExchangeRateResponse)okResult.Value;

        Assert.NotNull(responseParsed);
        Assert.NotNull(responseParsed.FromCurrencyName);
        Assert.NotNull(responseParsed.ToCurrencyName);
        Assert.True(responseParsed.ExchangeRate > 0m);
    }

    [Fact]
    public async Task GetExchangeRate_InvalidInput_FromCurrency_Error()
    {
        //Act
        var response = await _controller.GetExchangeRate(new ExchangeRateRequest
        {
            FromCurrency = "DWKAJNAWKJDN",
            ToCurrency = "JPY"
        });

        //Assert
        Assert.IsType<BadRequestObjectResult>(response);
        var badRequestResult = (BadRequestObjectResult)response;

        Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        var responseParsed = (List<ValidationFailure>)badRequestResult.Value;

        Assert.NotEmpty(responseParsed);
    }

    [Fact]
    public async Task GetExchangeRate_InvalidInput_ToCurrency_Error()
    {
        //Act
        var response = await _controller.GetExchangeRate(new ExchangeRateRequest
        {
            FromCurrency = "USD",
            ToCurrency = "X"
        });

        //Assert
        Assert.IsType<BadRequestObjectResult>(response);
        var badRequestResult = (BadRequestObjectResult)response;

        Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        var responseParsed = (List<ValidationFailure>)badRequestResult.Value;

        Assert.NotEmpty(responseParsed);
    }
}
