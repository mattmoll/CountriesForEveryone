using System.ComponentModel.DataAnnotations;

namespace CountriesForEveryone.Shared.Criteria
{
    public class LanguageCriteriaDto
    {
        [StringLength(30)]
        [AlphanumericWithSpacesAttribute]
        public string? LanguageName { get; set; }

        [StringLength(3)]
        public string? LanguageCode { get; set; }
    }
}
