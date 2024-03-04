using AV.ForeignExchangeRates.Application.Models.Request;
using FluentValidation;

namespace AV.ForeignExchangeRates.Application.Validators;

public class UpdateNameRequestValidator : AbstractValidator<UpdateNameRequest>
{
    public UpdateNameRequestValidator()
    {
        RuleFor(p => p.Code).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
        RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(64);
    }
}
