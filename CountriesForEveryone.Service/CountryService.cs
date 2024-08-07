using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Services;
using Microsoft.Extensions.Logging;

namespace CountriesForEveryone.Service
{
    public class CountryService : BaseService<CountryService>, ICountryService
    {
        private readonly ICountryAdapter _countryAdapter;

        public CountryService(ICountryAdapter countryAdapter, ILogger<CountryService> logger) : base(logger)
        {
            _countryAdapter = countryAdapter ?? throw new ArgumentNullException(nameof(countryAdapter));
        }

        public async Task<Country> Get(string countryCode)
        {
            return await TryExecute(() => _countryAdapter.Get(countryCode));
        }
    }
}
