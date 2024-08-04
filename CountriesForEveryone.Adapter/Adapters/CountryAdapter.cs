using AutoMapper;
using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.Adapters
{
    public class CountryAdapter : BaseAdapter<Country>, ICountryAdapter
    {
        public CountryAdapter(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
        {
        }

        public Task<Country> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetByCriteria(CountryCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
