namespace CountriesForEveryone.Adapter.Models
{
    public class CountryDto
    {
        public string Cca2 { get; set; }
        public string Cca3 { get; set; }
        public string Ccn3 { get; set; }
        public NameDto Name { get; set; }
        public bool UnMember { get; set; }
        public List<string> Capital { get; set; }
    }
}
