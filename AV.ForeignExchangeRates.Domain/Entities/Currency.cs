using AV.ForeignExchangeRates.Domain.Validators;
using FluentValidation;

namespace AV.ForeignExchangeRates.Domain.Entities;

public class Currency
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Currency() { } // for ef

    public Currency(Guid id, string code, string name)
    {
        Id = id;
        Code = code;
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        new CurrencyValidator().ValidateAndThrow(this);
    }

    public void SetName(string name)
    {
        Name = name;

        new CurrencyValidator().ValidateAndThrow(this);
    }
}