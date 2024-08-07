﻿namespace CountriesForEveryone.Core.Entities
{
    public class Country
    {
        public string Alpha2Code { get; set; }
        public string NumericCode { get; set; }
        public string Alpha3Code { get; set; }

        public string Name { get; set; }
        public string OfficialName { get; set; }
        public bool UnitedNationsMember { get; set; }
        public string CapitalCity { get; set; }
    }
}
