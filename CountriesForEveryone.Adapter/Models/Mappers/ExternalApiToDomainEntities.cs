using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.Models.Mappers
{
    public class ExternalApiToDomainEntities : AutoMapper.Profile
    {
        public ExternalApiToDomainEntities() 
        {
            CreateMap<CountryDto, Country>()
                .ForMember(dest => dest.Name, source => source.MapFrom(x => x.Name.Common))
                .ForMember(dest => dest.OfficialName, source => source.MapFrom(x => x.Name.Official))
                .ForMember(dest => dest.Alpha2Code, source => source.MapFrom(x => x.Cca2))
                .ForMember(dest => dest.Alpha3Code, source => source.MapFrom(x => x.Cca3))
                .ForMember(dest => dest.NumericCode, source => source.MapFrom(x => x.Ccn3))
                .ForMember(dest => dest.UnitedNationsMember, source => source.MapFrom(x => x.UnMember))
                .ForMember(dest => dest.CapitalCity, source => source.MapFrom(x => x.Capital.FirstOrDefault()));
        }
    }
}
