using System.ComponentModel.DataAnnotations;

namespace CountriesForEveryone.Shared.Criteria
{
    public class CountryCriteriaDto
    {
        [StringLength(50)]
        [AlphanumericWithSpacesAttribute]
        public string? Name { get; set; }

        [StringLength(30)]
        [AlphanumericWithSpacesAttribute]
        public string? CapitalCity { get; set; }

        public bool? UnitedNationsMember { get; set; }

        public bool? Independent { get; set; }

        [StringLength(20)]
        [AlphanumericWithSpacesAttribute]
        public string? Status { get; set; }

        [StringLength(10)]
        [AlphanumericWithSpacesAttribute]
        public string? RegionName { get; set; }

        [StringLength(30)]
        [AlphanumericWithSpacesAttribute]
        public string? LanguageName { get; set; }
    }
}
