﻿namespace CountriesForEveryone.Adapter.Models
{
    public class CountryDto
    {
        public NameDto Name { get; set; }
        public List<string> Tld { get; set; }
        public string Cca2 { get; set; }
        public string Ccn3 { get; set; }
        public string Cca3 { get; set; }
        public string Cioc { get; set; }
        public bool Independent { get; set; }
        public string Status { get; set; }
        public bool UnMember { get; set; }
        public Dictionary<string, CurrencyDto> Currencies { get; set; }
        public InternationalDialingCodeDto Idd { get; set; }
        public List<string> Capital { get; set; }
        public List<string> AltSpellings { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public Dictionary<string, TranslationDto> Translations { get; set; }
        public List<double> Latlng { get; set; }
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
