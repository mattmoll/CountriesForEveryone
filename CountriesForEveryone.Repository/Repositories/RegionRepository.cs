using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CountriesForEveryone.Repository.Repositories
{
    public class RegionRepository : BaseRepository, IRegionRepository
    {
        private readonly CountriesForEveryoneContext _context;

        public RegionRepository(CountriesForEveryoneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task InsertAll(IEnumerable<Region> regionsToUpsert)
        {
            if (regionsToUpsert == null) throw new ArgumentNullException(nameof(regionsToUpsert));

            foreach (var region in regionsToUpsert)
            {
                var existingRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Name == region.Name);

                if (existingRegion == null)
                {
                    region.Id = Guid.NewGuid();
                    _context.Regions.Add(region);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }
    }
}
