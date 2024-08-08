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
            return await TryExecute(async () =>
            {
                if (countryCode.Length < 2 || countryCode.Length > 3 || !countryCode.All(char.IsLetterOrDigit))
                    throw new Core.Exceptions.InvalidParameterException("Country Code must be alpha code 2 letters, alpha code 3 letters or ISO 3166-1 numeric code (3 numbers)");

                return await _countryAdapter.Get(countryCode);
            });
        }
    }
}
