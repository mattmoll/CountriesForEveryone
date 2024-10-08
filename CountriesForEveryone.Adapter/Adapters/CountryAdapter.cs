﻿using AutoMapper;
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

        public async Task<IEnumerable<Country>> GetAll()
        {
            try
            {
                var response = await HttpClient.GetAsync($"all");
                response.EnsureSuccessStatusCode();

                var responseDtos = await response.Content.ReadFromJsonAsync<CountryDto[]>();
                if (responseDtos == null || responseDtos.Length == 0)
                {
                    throw new Exception("No country data found.");
                }

                return Mapper.Map<IEnumerable<CountryDetails>>(responseDtos);
            }
            catch (Exception ex)
            {
                throw new ExternalApiException("Error fetching the list of all countries during execution of GetAll method", ex);
            }
        }

        public async Task<CountryDetails> Get(string countryCode)
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

                return Mapper.Map<CountryDetails>(responseDto);
            }
            catch (Exception ex)
            {
                throw new ExternalApiException("Error fetching a country during execution of Get method", ex);
            }
        }
    }
}
