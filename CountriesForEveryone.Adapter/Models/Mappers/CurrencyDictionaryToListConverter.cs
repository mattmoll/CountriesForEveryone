using AutoMapper;
using CountriesForEveryone.Adapter.Models;
using CountriesForEveryone.Core.Entities;

public class CurrencyDictionaryToListConverter : ITypeConverter<Dictionary<string, CurrencyDto>, List<Currency>>
{
    public List<Currency> Convert(Dictionary<string, CurrencyDto> source, List<Currency> destination, ResolutionContext context)
    {
        return source.Select(kv => new Currency
        {
            Code = kv.Key,
            Name = kv.Value.Name,
            Symbol = kv.Value.Symbol
        }).ToList();
    }
}
