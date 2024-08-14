using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Repositories
{
    public interface ILanguageRepository
    {
        public Task<IEnumerable<Language>> GetAll();
        public Task InsertAll(IEnumerable<Language> languagesToUpsert);
    }
}
