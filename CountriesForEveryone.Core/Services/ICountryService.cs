﻿using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Core.Services
{
    public interface ICountryService
    {
        Task<CountryDetails> Get(string countryCode);
        Task<PagedList<Country>> GetAllPaginated(FilterCriteria<CountryCriteria> filterCriteria);
    }
}
