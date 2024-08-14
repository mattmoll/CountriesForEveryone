using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Repositories
{
    public interface IRegionRepository
    {
        public Task<IEnumerable<Region>> GetAll();
        public Task InsertAll(IEnumerable<Region> regionsToInsert);
    }
}
