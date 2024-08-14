namespace CountriesForEveryone.Shared
{
    public class RegionWithCountriesDto : RegionDto
    {
        public List<CountryDto> Countries { get; set; }
    }
}
