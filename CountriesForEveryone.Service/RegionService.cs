using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Core.Services;
using Microsoft.Extensions.Logging;

namespace CountriesForEveryone.Service
{
    public class RegionService : BaseService<RegionService>, IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;

        public RegionService(IRegionRepository regionAdapter, ICountryRepository countryRepository, ILogger<RegionService> logger) : base(logger)
        {
            _regionRepository = regionAdapter ?? throw new ArgumentNullException(nameof(regionAdapter));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        public async Task<List<Region>> GetAllByCriteria(RegionCriteria regionCriteria)
        {
            return await TryExecute(async () =>
            {
                var regions = await _regionRepository.GetAll();
                IEnumerable<Region> filteredRegions = regions;

                if (regionCriteria.RegionName != null)
                {
                    filteredRegions = regions.Where(r => r.Name.ToLowerInvariant() == regionCriteria.RegionName.ToLowerInvariant());
                }

                if (filteredRegions.Any()) 
                {
                    var filterCriteria = new FilterCriteria<CountryCriteria>()
                    {
                        PageSize = int.MaxValue,
                        PageNumber = 1,
                        Criteria = new CountryCriteria()
                        {
                            RegionName = regionCriteria.RegionName,
                            UnitedNationsMember = regionCriteria.CountriesUnitedNationsMember,
                            Name = regionCriteria.CountryName,
                        }
                    };

                    var countries = await _countryRepository.GetByCriteria(filterCriteria);

                    if (countries != null && countries.TotalItemCount > 0)
                    {
                        foreach (var region in filteredRegions)
                        {
                            region.Countries = countries.Items.Where(c => c.Region.Name == region.Name).ToList();
                        }
                    }
                }

                return filteredRegions.ToList();
            });
        }
    }
}
