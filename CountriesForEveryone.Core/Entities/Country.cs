namespace CountriesForEveryone.Core.Entities
{
    public class Country
    {
        public string Alpha2Code { get; set; }
        public string NumericCode { get; set; }
        public string Alpha3Code { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public bool UnitedNationsMember { get; set; }
        public bool Independent { get; set; }
        public string Status { get; set; }
        public string CapitalCity { get; set; }
        public Region Region { get; set; }
        public List<Language> Languages { get; set; }
    }
}
