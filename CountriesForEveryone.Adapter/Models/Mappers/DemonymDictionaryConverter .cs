﻿using AutoMapper;
using CountriesForEveryone.Adapter.Models;
using CountriesForEveryone.Core.Entities;

public class DemonymDictionaryConverter : ITypeConverter<Dictionary<string, DemonymDto>, Dictionary<string, Demonym>>
{
    public Dictionary<string, Demonym> Convert(Dictionary<string, DemonymDto> source, Dictionary<string, Demonym> destination, ResolutionContext context)
    {
        if (source == null || source.Count() == 0) return new Dictionary<string, Demonym>();

        return source.ToDictionary(
            item => item.Key,
            item => new Demonym
            {
                F = item.Value.F,
                M = item.Value.M
            });
    }
}
