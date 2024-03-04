using AV.ForeignExchangeRates.Domain.Entities;

namespace AV.ForeignExchangeRates.Domain.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Currency? GetById(Guid id);
        Currency? GetByCode(string code);
        Currency Save(Currency currency);
        void Update(Currency currency);
        void Delete(Currency currency);
    }
}
