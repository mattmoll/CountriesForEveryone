﻿using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Extensions;
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

        public Task<PagedList<Country>> GetByCriteria(FilterCriteria<CountryCriteria> filterCriteria)
        {
            var countries = GetCountriesWithRelatedEntities();

            countries = FilterCountries(filterCriteria, countries);

            var orderedEntities = Order(countries, filterCriteria);
            var pagedEntities = orderedEntities.Paginate(filterCriteria.PageSize, filterCriteria.PageNumber);

            var result = new PagedList<Country>()
            {
                Items = pagedEntities.ToList(),
                TotalItemCount = countries.Count()
            };

            return Task.FromResult(result);
        }

        private static IQueryable<Country> FilterCountries(FilterCriteria<CountryCriteria> filterCriteria, IQueryable<Country> countries)
        {
            var criteria = filterCriteria.Criteria;

            if (criteria == null)
            {
                criteria = new CountryCriteria();
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                countries = countries.Where(c => c.Name == criteria.Name);
            }

            if (!string.IsNullOrEmpty(criteria.CapitalCity))
            {
                countries = countries.Where(c => c.CapitalCity == criteria.CapitalCity);
            }

            if (criteria.UnitedNationsMember.HasValue)
            {
                countries = countries.Where(c => c.UnitedNationsMember == criteria.UnitedNationsMember);
            }

            if (criteria.Independent.HasValue)
            {
                countries = countries.Where(c => c.Independent == criteria.Independent);
            }

            if (!string.IsNullOrEmpty(criteria.Status))
            {
                countries = countries.Where(c => c.Status == criteria.Status);
            }

            if (!string.IsNullOrEmpty(criteria.RegionName))
            {
                countries = countries.Where(c => c.Region != null && c.Region.Name == criteria.RegionName);
            }

            if (!string.IsNullOrEmpty(criteria.LanguageName))
            {
                countries = countries.Where(c => c.Languages.Any(l => l.Name == criteria.LanguageName));
            }

            return countries;
        }

        private static IEnumerable<Country> Order(IEnumerable<Country> filteredEntities, FilterCriteria<CountryCriteria> filterCriteria)
        {
            return filterCriteria.SortBy?.ToLowerInvariant() switch
            {
                "Alpha2Code" => filteredEntities.OrderEntitiesBy(x => x.Alpha2Code, filterCriteria.OrderDirection),
                "Alpha3Code" => filteredEntities.OrderEntitiesBy(x => x.Alpha3Code, filterCriteria.OrderDirection),
                "NumericCode" => filteredEntities.OrderEntitiesBy(x => x.NumericCode, filterCriteria.OrderDirection),
                "CapitalCity" => filteredEntities.OrderEntitiesBy(x => x.CapitalCity, filterCriteria.OrderDirection),
                "Name" => filteredEntities.OrderEntitiesBy(x => x.Name, filterCriteria.OrderDirection),
                _ => filteredEntities.OrderEntitiesBy(x => x.Name, filterCriteria.OrderDirection)
            };
        }


        private IQueryable<Country> GetCountriesWithRelatedEntities()
        {
            return _context.Countries;
                //.Include(t => t.Region)
                //.Include(t => t.Languages)
                //.AsSplitQuery();
        }
    }
}
