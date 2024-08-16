using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CountriesForEveryone.Service
{
    public class CountryService : BaseService<CountryService>, ICountryService
    {
        private readonly ICountryAdapter _countryAdapter;
        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _cache;

        public CountryService(ICountryAdapter countryAdapter, ICountryRepository countryRepository, IMemoryCache cache, ILogger<CountryService> logger) : base(logger)
        {
            _countryAdapter = countryAdapter ?? throw new ArgumentNullException(nameof(countryAdapter));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<CountryDetails> Get(string countryCode)
        {
            return await TryExecute(async () =>
            {
                if (countryCode.Length < 2 || countryCode.Length > 3 || !countryCode.All(char.IsLetterOrDigit))
                    throw new Core.Exceptions.InvalidParameterException("Country Code must be alpha code 2 letters, alpha code 3 letters or ISO 3166-1 numeric code (3 numbers)");

                var cacheKey = $"CountryDetails-{countryCode}";
                _cache.TryGetValue(cacheKey, out CountryDetails? countryDetails);

                if (countryDetails is null)
                {
                    countryDetails = await _countryAdapter.Get(countryCode);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
                    _cache.Set(cacheKey, countryDetails, cacheEntryOptions);
                }

                return countryDetails;
            });
        }

        public async Task<PagedList<Country>> GetAllPaginated(FilterCriteria<CountryCriteria> filterCriteria)
        {
            return await TryExecute(async () =>
            {
                var results = await _countryRepository.GetByCriteria(filterCriteria);

                return results;
            });
        }
    }
}
