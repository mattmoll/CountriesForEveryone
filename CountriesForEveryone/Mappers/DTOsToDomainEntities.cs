using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Shared;
using CountriesForEveryone.Shared.Criteria;

namespace CountriesForEveryone.Server.Mappers
{
    public class DTOsToDomainEntities : AutoMapper.Profile
    {
        public DTOsToDomainEntities() 
        {
            CreateMap(typeof(PagingRequestDto<>), typeof(FilterCriteria<>));
            CreateMap<CountryCriteriaDto, CountryCriteria>();
        }
    }
}
