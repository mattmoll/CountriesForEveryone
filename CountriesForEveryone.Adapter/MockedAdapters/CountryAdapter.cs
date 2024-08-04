using CountriesForEveryone.Adapter.Extensions;
using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.MockedAdapters
{
    public class CountryAdapter : MockAdapterBase<Country, CountryCriteria>, ICountryAdapter
    {
        public Task<IEnumerable<Country>> GetByCriteria(CountryCriteria criteria)
        {
            throw new NotImplementedException();
        }

        protected override List<Country> LoadMockedEntities()
        {
            return new()
            {
                new()
                {
                    Id = Guid.Parse("58048bb3-f0d9-4dc3-bfeb-c60be7ab0c02"),
                    Name = "United States",
                },
                new()
                {
                    Id = Guid.Parse("E88C7882-D73F-4A05-A134-D08E6251C713"),
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

        protected override Guid GetIdProperty(Country x) => x.Id;

        protected override IEnumerable<Country> Order(FilterCriteria<CountryCriteria> filterCriteria, IEnumerable<Country> filteredEntities)
        {
            return filteredEntities.OrderEntitiesBy(s => s.Name, filterCriteria.OrderDirection);
        }
    }
}
