using CountriesForEveryone.Core.Entities;

namespace CountriesForEveryone.Service.Test.Mocks
{
    internal static class Countries
    {
        internal static Country GetOne()
        {
            return new Country()
            {
                Alpha2Code = "AR",
                Name = "Argentina",
                CapitalCity = "CABA",
                UnitedNationsMember = true
            };
        }
    }
}
