using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Services;
using Microsoft.Extensions.Logging;

namespace CountriesForEveryone.Service
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly ICountryAdapter _countryAdapter;

        public CountryService(ICountryAdapter countryAdapter, ILogger logger) : base(logger)
        {
            _countryAdapter = countryAdapter ?? throw new ArgumentNullException(nameof(countryAdapter));
        }
    }
}
