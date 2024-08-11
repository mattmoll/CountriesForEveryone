using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Extensions;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CountriesForEveryone.Repository.Repositories
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        private readonly CountriesForEveryoneContext _context;

        public CountryRepository(CountriesForEveryoneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<PagedList<Country>> GetByCriteria(FilterCriteria<CountryCriteria> filterCriteria)
        {
            var countries = GetCountryWithRelatedEntities();

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


        private IQueryable<Country> GetCountryWithRelatedEntities()
        {
            return _context.Countries
                .Include(t => t.Region)
                .Include(t => t.Languages)
                .AsSplitQuery();
        }
    }
}
