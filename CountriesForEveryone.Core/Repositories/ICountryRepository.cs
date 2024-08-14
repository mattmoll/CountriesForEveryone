using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Repositories
{
    public interface ICountryRepository
    {
        public Task<PagedList<Country>> GetByCriteria(FilterCriteria<CountryCriteria> filterCriteria);
        public Task InsertAll(IEnumerable<Country> countriesToUpsert);
    }
}
