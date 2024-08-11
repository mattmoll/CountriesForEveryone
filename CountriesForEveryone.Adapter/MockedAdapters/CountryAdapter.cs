using CountriesForEveryone.Adapter.Extensions;
using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.MockedAdapters
{
    public class CountryAdapter : MockAdapterBase<Country, CountryCriteria>, ICountryAdapter
    {
        public Task<CountryDetails> Get(string countryCode)
        {
            return Task.FromResult(new CountryDetails(Entities.First(x => x.Alpha2Code == countryCode)));
        }

        protected override List<Country> LoadMockedEntities()
        {
            return new()
            {
                new()
                {
                    Alpha2Code = "US",
                    Name = "United States",
                },
                new()
                {
                    Alpha2Code = "AR",
                    Name = "Argentina",
                },
            };
        }

        protected override IEnumerable<Country> Filter(FilterCriteria<CountryCriteria> filterCriteria, IEnumerable<Country> entities)
        {
            var criteria = filterCriteria.Criteria;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                entities = entities.Where(e => e.Name == criteria.Name);
            }

            return entities;
        }

        protected override IEnumerable<Country> Order(FilterCriteria<CountryCriteria> filterCriteria, IEnumerable<Country> filteredEntities)
        {
            return filteredEntities.OrderEntitiesBy(s => s.Name, filterCriteria.OrderDirection ?? OrderDirection.Descending);
        }
    }
}
