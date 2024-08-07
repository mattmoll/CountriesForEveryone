using CountriesForEveryone.Adapter.Adapters;
using CountriesForEveryone.Adapter.Settings;
using CountriesForEveryone.Core.Adapters;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapters(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        var adapterHttpSettings = new AdapterHttpSettings();
        configuration.Bind(adapterHttpSettings);

        services.AddApiClient<ICountryAdapter, CountryAdapter>(new Uri(adapterHttpSettings.CountriesApiUrl));
        return services;
    }

    public static IServiceCollection AddMockedHttpCommandAdapters(this IServiceCollection services)
    {
        services.AddTransient<ICountryAdapter, CountriesForEveryone.Adapter.MockedAdapters.CountryAdapter>();
        return services;
    }

    private static void AddApiClient<TInterface, TClass>(this IServiceCollection services, Uri baseAddress)
    where TInterface : class
    where TClass : class, TInterface
    {
        services.AddHttpClient<TInterface, TClass>(x =>
        {
            x.BaseAddress = baseAddress;
        });
    }
}
