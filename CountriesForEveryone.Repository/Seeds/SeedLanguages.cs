using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Repository.Seeds
{
    public static class SeedLanguages
    {
        public static Language GetSpanishLanguage()
        {
            return new Language()
            {
                Id = Guid.Parse("ff7da6a5-9e0e-4703-9c59-e1620e1abcf8"),
                Code = "spa",
                Name = "Spanish",
            };
        }
    }
}
