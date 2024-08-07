using CountriesForEveryone.Core.Services;
using CountriesForEveryone.Service;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<ICountryService, CountryService>();
        return services;
    }
}

