using CountriesForEveryone.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataBaseConfiguration
    {
        public static void InitializeDataBase(this IApplicationBuilder app, IConfiguration configuration)
        {
            DatabaseInitialization.Initialize(app.ApplicationServices, configuration);
        }
    }
}
