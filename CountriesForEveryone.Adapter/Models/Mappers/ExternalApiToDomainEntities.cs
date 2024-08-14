using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Adapter.Models.Mappers
{
    public class EsourceternalApiToDomainEntities : AutoMapper.Profile
    {
        public EsourceternalApiToDomainEntities() 
        {
            CreateMap<CountryDto, Country>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name.Common))
                .ForMember(dest => dest.OfficialName, opt => opt.MapFrom(source => source.Name.Official))
                .ForMember(dest => dest.Alpha2Code, opt => opt.MapFrom(source => source.Cca2))
                .ForMember(dest => dest.Alpha3Code, opt => opt.MapFrom(source => source.Cca3))
                .ForMember(dest => dest.NumericCode, opt => opt.MapFrom(source => source.Ccn3 ?? ""))
                .ForMember(dest => dest.UnitedNationsMember, opt => opt.MapFrom(source => source.UnMember))
                .ForMember(dest => dest.Independent, opt => opt.MapFrom(source => source.Independent))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(source => source.Status))
                .ForMember(dest => dest.CapitalCity, opt => opt.MapFrom(source => source.Capital.FirstOrDefault() ?? ""))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(source => new Region(source.Region)))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(source => source.Languages == null ? new List<Language>()
                                                                                : new List<Language>(source.Languages.Select(l => new Language(l.Key, l.Value)))));

            CreateMap<Dictionary<string, CurrencyDto>, List<Currency>>().ConvertUsing(new CurrencyDictionaryToListConverter());
            CreateMap<Dictionary<string, TranslationDto>, Dictionary<string, Translation>>().ConvertUsing<TranslationDictionaryConverter>();
            CreateMap<Dictionary<string, DemonymDto>, Dictionary<string, Demonym>>().ConvertUsing<DemonymDictionaryConverter>();

            CreateMap<CountryDto, CountryDetails>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name.Common))
                .ForMember(dest => dest.OfficialName, opt => opt.MapFrom(source => source.Name.Official))
                .ForMember(dest => dest.Alpha2Code, opt => opt.MapFrom(source => source.Cca2))
                .ForMember(dest => dest.Alpha3Code, opt => opt.MapFrom(source => source.Cca3))
                .ForMember(dest => dest.NumericCode, opt => opt.MapFrom(source => source.Ccn3 ?? ""))
                .ForMember(dest => dest.UnitedNationsMember, opt => opt.MapFrom(source => source.UnMember))
                .ForMember(dest => dest.Independent, opt => opt.MapFrom(source => source.Independent))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(source => source.Status))
                .ForMember(dest => dest.CapitalCity, opt => opt.MapFrom(source => source.Capital.FirstOrDefault() ?? ""))
                .ForMember(dest => dest.InternationalDialingCodes, opt => opt.MapFrom(source => source.Idd))
                .ForMember(dest => dest.InternationalTopLevelDomains, opt => opt.MapFrom(source => source.Tld))
                .ForMember(dest => dest.CapitalLatitudeLongitude, opt => opt.MapFrom(source => source.Latlng))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(source => new Region(source.Region)))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(source => source.Languages == null ? new List<Language>() 
                                                                                : new List<Language>(source.Languages.Select(l => new Language(l.Key, l.Value)))))
                    .AfterMap((src, dest, ctx) =>
                    {
                        dest.Translations = ctx.Mapper.Map<Dictionary<string, Translation>>(src.Translations);
                        dest.Demonyms = ctx.Mapper.Map<Dictionary<string, Demonym>>(src.Demonyms);
                    });

            CreateMap<CapitalInfoDto, CapitalInfo>();
            CreateMap<CarDto, Car>();
            CreateMap<CoatOfArmsDto, CoatOfArms>();
            CreateMap<CountryDto, Country>();
            CreateMap<CurrencyDto, Currency>();
            CreateMap<DemonymDto, Demonym>();
            CreateMap<FlagsDto, Flags>();
            CreateMap<InternationalDialingCodeDto, InternationalDialingCodes>();
            CreateMap<MapsDto, Maps>();
            CreateMap<NameDto, Name>();
            CreateMap<NativeNameDto, NativeName>();
            CreateMap<PostalCodeDto, PostalCode>();
            CreateMap<TranslationDto, Translation>();
        }
    }
}
