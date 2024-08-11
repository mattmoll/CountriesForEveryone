namespace CountriesForEveryone.Core.Entities
{
    public class CountryDetails : Country
    {
        public CountryDetails() { }

        public CountryDetails(Country country) 
        {
            Alpha2Code = country.Alpha2Code;
            NumericCode = country.NumericCode;
            Alpha3Code = country.Alpha3Code;
            Name = country.Name;
            OfficialName = country.OfficialName;
            UnitedNationsMember = country.UnitedNationsMember;
            Independent = country.Independent;
            Status = country.Status;
            CapitalCity = country.CapitalCity;
            Region = country.Region;
            Languages = country.Languages;
        }

        public List<string> InternationalTopLevelDomains { get; set; }
        public List<Currency> Currencies { get; set; }
        public InternationalDialingCodes InternationalDialingCodes { get; set; }
        public List<string> AlternateSpellingsCountryName { get; set; }
        public string Subregion { get; set; }
        public Dictionary<string, Translation> Translations { get; set; }
        public List<double> CapitalLatitudeLongitude { get; set; }
        public bool Landlocked { get; set; }
        public List<string> Borders { get; set; }
        public double Area { get; set; }
        public Dictionary<string, Demonym> Demonyms { get; set; }
        public string Flag { get; set; }
        public Maps Maps { get; set; }
        public long Population { get; set; }
        public Dictionary<string, double> Gini { get; set; }
        public string Fifa { get; set; }
        public Car Car { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Continents { get; set; }
        public Flags Flags { get; set; }
        public CoatOfArms CoatOfArms { get; set; }
        public string StartOfWeek { get; set; }
        public CapitalInfo CapitalInfo { get; set; }
        public PostalCode PostalCode { get; set; }
    }
}
