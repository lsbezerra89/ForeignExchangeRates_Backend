using AV.ForeignExchangeRates.Domain.Entities;
using FluentValidation;

namespace AV.ForeignExchangeRates.Domain.Validators;

public class CurrencyValidator : AbstractValidator<Currency>
{
    public CurrencyValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty();
        RuleFor(p => p.Code).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
        RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(64);
        RuleFor(p => p.CreatedAt).NotNull().NotEmpty();
        RuleFor(p => p.UpdatedAt).NotNull().NotEmpty();
    }
}
