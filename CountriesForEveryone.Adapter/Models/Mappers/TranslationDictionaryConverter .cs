using AutoMapper;
using CountriesForEveryone.Adapter.Models;
using CountriesForEveryone.Core.Entities;

public class TranslationDictionaryConverter : ITypeConverter<Dictionary<string, TranslationDto>, Dictionary<string, Translation>>
{
    public Dictionary<string, Translation> Convert(Dictionary<string, TranslationDto> source, Dictionary<string, Translation> destination, ResolutionContext context)
    {
        return source.ToDictionary(
            item => item.Key,
            item => new Translation
            {
                Official = item.Value.Official,
                Common = item.Value.Common
            });
    }
}
