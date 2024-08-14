using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Core.Services;
using Microsoft.Extensions.Logging;

namespace CountriesForEveryone.Service
{
    public class LanguageService : BaseService<LanguageService>, ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ICountryRepository _countryRepository;

        public LanguageService(ILanguageRepository languageRepository, ICountryRepository countryRepository, ILogger<LanguageService> logger) : base(logger)
        {
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        public async Task<List<Language>> GetAllByCriteria(LanguageCriteria languageCriteria)
        {
            return await TryExecute(async () =>
            {
                var languages = await _languageRepository.GetAll();
                IEnumerable<Language> filteredLanguages = languages;

                if (languageCriteria.LanguageName != null)
                {
                    filteredLanguages = languages.Where(l => l.Name == languageCriteria.LanguageName);
                }

                if (languageCriteria.LanguageCode != null)
                {
                    filteredLanguages = languages.Where(l => l.Code == languageCriteria.LanguageCode);

                    if (filteredLanguages.Any() && languageCriteria.LanguageName == null)
                    {
                        languageCriteria.LanguageName = filteredLanguages.First().Name;
                    }
                }

                if (filteredLanguages.Any())
                {
                    var filterCriteria = new FilterCriteria<CountryCriteria>()
                    {
                        PageSize = int.MaxValue,
                        PageNumber = 1,
                        Criteria = new CountryCriteria()
                        {
                            LanguageName = languageCriteria.LanguageName,
                        }
                    };

                    var countries = await _countryRepository.GetByCriteria(filterCriteria);

                    if (countries != null && countries.TotalItemCount > 0)
                    {
                        foreach (var language in filteredLanguages)
                        {
                            language.Countries = countries.Items.Where(c => c.Languages.Any(l => l.Name == language.Name)).ToList();
                        }
                    }
                }

                return filteredLanguages.ToList();
            });
        }
    }
}
