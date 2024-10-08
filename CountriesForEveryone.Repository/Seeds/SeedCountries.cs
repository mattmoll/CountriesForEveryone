﻿using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Repository.Contexts;

namespace CountriesForEveryone.Repository.Seeds
{
    public static class SeedCountries
    {

        public static void Seed(CountriesForEveryoneContext context)
        {
            var countries = GetCountries();
            foreach (var entity in countries)
            {
                if (context.Countries.Any(x => x.Id == entity.Id)) continue;

                context.Countries.Add(entity);
            }

            context.SaveChanges();
        }

        public static IEnumerable<Country> GetCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                     Id = Guid.Parse("1084e3f7-f820-44a0-ae77-753c880040a4"),
                     Alpha2Code = "AR",
                     Alpha3Code = "ARG",
                     CapitalCity = "CABA",
                     Independent = true,
                     UnitedNationsMember = true,
                     Name = "Argentina",
                     OfficialName = "Argentina",
                     NumericCode = "032",
                     Status = "officially-assigned",
                     Region = SeedRegions.GetAmericaRegion(),
                     Languages = new List<Language>() { SeedLanguages.GetSpanishLanguage() }
                }
            };
        }
    }
}
