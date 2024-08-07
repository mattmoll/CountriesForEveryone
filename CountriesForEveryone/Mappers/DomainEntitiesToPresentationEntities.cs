using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Shared;

namespace CountriesForEveryone.Server.Mappers
{
    public class DomainEntitiesToPresentationEntities : AutoMapper.Profile
    {
        public DomainEntitiesToPresentationEntities() 
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
