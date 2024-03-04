using AV.ForeignExchangeRates.Application.Models.Request;
using FluentValidation;

namespace AV.ForeignExchangeRates.Application.Validators;

public class ExchangeRateRequestValidator : AbstractValidator<ExchangeRateRequest>
{
	public ExchangeRateRequestValidator()
	{
		RuleFor(p => p.FromCurrency).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
		RuleFor(p => p.ToCurrency).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
    }
}
