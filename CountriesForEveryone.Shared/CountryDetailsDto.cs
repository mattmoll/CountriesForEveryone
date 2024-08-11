namespace CountriesForEveryone.Shared
{
    public class CountryDetailsDto : CountryDto
    {
        public List<string> InternationalTopLevelDomains { get; set; }
        public List<CurrencyDto> Currencies { get; set; }
        public InternationalDialingCodesDto InternationalDialingCodes { get; set; }
        public List<string> AlternateSpellingsCountryName { get; set; }
        public string Subregion { get; set; }
        public Dictionary<string, TranslationDto> Translations { get; set; }
        public List<double> CapitalLatitudeLongitude { get; set; }
        public bool Landlocked { get; set; }
        public List<string> Borders { get; set; }
        public double Area { get; set; }
        public Dictionary<string, DemonymDto> Demonyms { get; set; }
        public string Flag { get; set; }
        public MapsDto Maps { get; set; }
        public long Population { get; set; }
        public Dictionary<string, double> Gini { get; set; }
        public string Fifa { get; set; }
        public CarDto Car { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Continents { get; set; }
        public FlagsDto Flags { get; set; }
        public CoatOfArmsDto CoatOfArms { get; set; }
        public string StartOfWeek { get; set; }
        public CapitalInfoDto CapitalInfo { get; set; }
        public PostalCodeDto PostalCode { get; set; }
    }
}
