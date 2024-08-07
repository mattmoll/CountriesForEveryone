using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Services
{
    public interface ICountryService
    {
        Task<Country> Get(string countryCode);
    }
}
