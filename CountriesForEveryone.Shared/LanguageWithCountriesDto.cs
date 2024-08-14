namespace CountriesForEveryone.Shared
{
    public class LanguageWithCountriesDto : LanguageDto
    {
        public List<CountryDto> Countries { get; set; }
    }
}
