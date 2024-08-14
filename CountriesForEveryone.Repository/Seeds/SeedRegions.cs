using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Repository.Seeds
{
    public static class SeedRegions
    {
        public static Region GetAmericaRegion()
        {
            return new Region()
            {
                Id = Guid.Parse("6599df97-8d1f-4996-a131-6bce99befe95"),
                Name = "America",
            };
        }
    }
}
