using AV.ForeignExchangeRates.Application.Models.Request;
using FluentValidation;

namespace AV.ForeignExchangeRates.Application.Validators;

public class CurrencyRequestValidator : AbstractValidator<CurrencyRequest>
{
    public CurrencyRequestValidator()
    {
        RuleFor(p => p.Code).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
    }
}
