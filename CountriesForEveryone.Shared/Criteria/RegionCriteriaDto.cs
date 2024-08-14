using System.ComponentModel.DataAnnotations;

namespace CountriesForEveryone.Shared.Criteria
{
    public class RegionCriteriaDto
    {
        [StringLength(10)]
        [AlphanumericWithSpacesAttribute]
        public string? RegionName { get; set; }

        public bool? CountriesUnitedNationsMember { get; set; }

        [StringLength(50)]
        [AlphanumericWithSpacesAttribute]
        public string? CountryName { get; set; }
    }
}
