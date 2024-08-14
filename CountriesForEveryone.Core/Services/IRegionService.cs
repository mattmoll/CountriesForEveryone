using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Services
{
    public interface IRegionService
    {
        Task<List<Region>> GetAllByCriteria(RegionCriteria regionCriteria);
    }
}
