using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Services
{
    public interface ILanguageService
    {
        Task<List<Language>> GetAllByCriteria(LanguageCriteria languageCriteria);
    }
}
