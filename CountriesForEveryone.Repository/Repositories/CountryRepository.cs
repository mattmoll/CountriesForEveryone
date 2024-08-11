using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;

namespace CountriesForEveryone.Repository.Repositories
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        public Task<PagedList<Country>> GetPaginated(FilterCriteria<CountryCriteria> filterCriteria)
        {
            throw new NotImplementedException();
        }
    }
}
