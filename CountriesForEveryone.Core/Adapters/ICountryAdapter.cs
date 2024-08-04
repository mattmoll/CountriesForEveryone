using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Adapters
{
    public interface ICountryAdapter
    {
        Task<Country> Get(Guid id);
        Task<IEnumerable<Country>> GetByCriteria(CountryCriteria criteria);
    }
}
