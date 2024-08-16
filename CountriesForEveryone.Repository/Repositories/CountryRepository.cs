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

        public async Task InsertAll(IEnumerable<Country> countriesToUpsert)
        {
            if (countriesToUpsert == null) throw new ArgumentNullException(nameof(countriesToUpsert));

            foreach (var country in countriesToUpsert)
            {
                var existingCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Alpha2Code == country.Alpha2Code);

                if (existingCountry == null)
                {
                    country.Id = Guid.NewGuid();
                    _context.Countries.Add(country);
                }
            }

            await _context.SaveChangesAsync();
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
                countries = countries.Where(c => c.CapitalCity != null && c.CapitalCity == criteria.CapitalCity);
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
                countries = countries.Where(c => c.Status != null && c.Status == criteria.Status);
            }

            if (!string.IsNullOrEmpty(criteria.RegionName))
            {
                countries = countries.Where(c => c.Region != null && c.Region.Name == criteria.RegionName);
            }

            if (!string.IsNullOrEmpty(criteria.LanguageName))
            {
                countries = countries.Where(c => c.Languages.Any(l => l != null && l.Name == criteria.LanguageName));
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
            return _context.Countries
                .Include(t => t.Region)
                .Include(t => t.Languages)
                .AsSplitQuery();
        }
    }
}
