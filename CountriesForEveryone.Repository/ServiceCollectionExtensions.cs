using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Repository.Factories;
using CountriesForEveryone.Repository.Repositories;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddTransient<ICountryRepository, CountryRepository>()
                .AddTransient<IRegionRepository, RegionRepository>()
                .AddTransient<ILanguageRepository, LanguageRepository>()
            ;

        public static IServiceCollection AddCountriesForEveryoneContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("CountriesForEveryoneConnectionString");

            if (string.IsNullOrEmpty(connString))
                throw new Exception("Missing Configuration: 'CountriesForEveryoneConnectionString' must be provided in appSettings.json");

            var selectedConnection = configuration.GetValue<String>("SelectedConnection");

            if (string.IsNullOrEmpty(selectedConnection))
                throw new Exception("Missing Configuration: 'SelectedConnection' must be provided in appSettings.json");

            var engine = RepositoryEngineFactory.CreateEngineFromConnectionName(selectedConnection);
            engine.AddDbContext(connString, services);

            return services;
        }
    }
}
