using AutoMapper;
using CountriesForEveryone.Adapter.Models;
using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Exceptions;
using System.Net.Http.Json;

namespace CountriesForEveryone.Adapter.Adapters
{
    public class CountryAdapter : BaseAdapter<Country>, ICountryAdapter
    {
        public CountryAdapter(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
        {
        }

        public async Task<Country> Get(string countryCode)
        {
            try
            {
                var response = await HttpClient.GetAsync($"alpha/{countryCode}");
                response.EnsureSuccessStatusCode();

                var responseDtos = await response.Content.ReadFromJsonAsync<CountryDto[]>();
                if (responseDtos == null || responseDtos.Length == 0)
                {
                    throw new Exception("No country data found.");
                }
                var responseDto = responseDtos[0];

                return Mapper.Map<Country>(responseDto);
            }
            catch (Exception ex)
            {
                throw new ExternalApiException("Error fetching a country during execution of Get method", ex);
            }
        }

        public Task<IEnumerable<Country>> GetByCriteria(CountryCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
