using AV.ForeignExchangeRates.Domain.Entities;
using FluentValidation;

namespace AV.ForeignExchangeRates.Tests.Domain;

public class CurrencyTests
{
    [Fact]
    public void CreateCurrency_Successfully()
    {
        var currency = new Currency(Guid.NewGuid(), "BRL", "Brazilian Real");

        Assert.NotNull(currency);
    }

    [Fact]
    public void CreateCurrency_WithError_InvalidId()
    {
        var exception = Assert.Throws<ValidationException>(() => { new Currency(new Guid(), "BRL", "Brazilian Real"); });

        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("AAAA")]
    public void CreateCurrency_WithError_InvalidCode(string code)
    {
        var exception = Assert.Throws<ValidationException>(() => { new Currency(Guid.NewGuid(), code, "Brazilian Real"); });

        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
    }
}
