using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Adapters
{
    public interface ICountryAdapter
    {
        Task<IEnumerable<Country>> GetAll();
        Task<CountryDetails> Get(string countryCode);
    }
}
