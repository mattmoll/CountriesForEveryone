﻿using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Adapters
{
    public interface ICountryAdapter
    {
        Task<CountryDetails> Get(string countryCode);
    }
}
