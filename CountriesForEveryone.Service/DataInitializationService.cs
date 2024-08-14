using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Core.Services;

namespace CountriesForEveryone.Service
{
    public class DataInitializationService : IDataInitializationService
    {
        private readonly ICountryAdapter _countryAdapter;
        private readonly ICountryRepository _countryRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ILanguageRepository _languageRepository;

        public DataInitializationService(ICountryAdapter countryAdapter, ICountryRepository countryRepository, IRegionRepository regionRepository, ILanguageRepository languageRepository)
        {
            _countryAdapter = countryAdapter ?? throw new ArgumentNullException(nameof(countryAdapter));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
        }

        public async Task LoadCountriesData(bool forceDataFetchingUpdate)
        {
            if (!forceDataFetchingUpdate)
            {
                var countriesInDB = await _countryRepository.GetByCriteria(new FilterCriteria<CountryCriteria>());

                // The setting to force is false and there is already data in the DB. No fetching or update needed.
                if (countriesInDB != null && countriesInDB.TotalItemCount != 0) return;
            }

            var countries = await _countryAdapter.GetAll();

            if (countries == null || countries.Count() == 0)
                return;

            var regionsToInsert = countries.Select(c => c.Region).DistinctBy(c => c.Name);
            await _regionRepository.InsertAll(regionsToInsert);
            var allRegions = await _regionRepository.GetAll();

            var languagesToInsert = countries.SelectMany(c => c.Languages).DistinctBy(c => c.Name);
            await _languageRepository.InsertAll(languagesToInsert);
            var allLanguages = await _languageRepository.GetAll();


            foreach (var item in countries)
            {
                item.Region = allRegions.First(r => r.Name == item.Region?.Name);
                item.Languages = item.Languages.Select(l => allLanguages.First(x => x.Name == l.Name)).ToList();
            }

            await _countryRepository.InsertAll(countries);
        }
    }
}
