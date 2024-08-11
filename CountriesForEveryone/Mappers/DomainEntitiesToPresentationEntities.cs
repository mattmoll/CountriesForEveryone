using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Shared;

namespace CountriesForEveryone.Server.Mappers
{
    public class DomainEntitiesToPresentationEntities : AutoMapper.Profile
    {
        public DomainEntitiesToPresentationEntities() 
        {
            CreateMap(typeof(PagedList<>), typeof(PagedResponseDto<>));

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDetails, CountryDetailsDto>();

            CreateMap<CapitalInfo, CapitalInfoDto>();
            CreateMap<Car, CarDto>();
            CreateMap<CoatOfArms, CoatOfArmsDto>();
            CreateMap<Currency, CurrencyDto>();
            CreateMap<Demonym, DemonymDto>();
            CreateMap<Flags, FlagsDto>();
            CreateMap<InternationalDialingCodes, InternationalDialingCodesDto>();
            CreateMap<Maps, MapsDto>();
            CreateMap<Name, NameDto>();
            CreateMap<NativeName, NativeNameDto>();
            CreateMap<PostalCode, PostalCodeDto>();
            CreateMap<Translation, TranslationDto>();
            CreateMap<Region, RegionDto>();
            CreateMap<Language, LanguageDto>();
        }
    }
}
