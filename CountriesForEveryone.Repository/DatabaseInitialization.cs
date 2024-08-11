using CountriesForEveryone.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesForEveryone.Repository
{
    public static class DatabaseInitialization
    {
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var serviceScope = serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<CountriesForEveryoneContext>();

            if (configuration["RunMigrations"] == "True")
                dbContext.Database.Migrate();

            if (configuration["RunSeeds"] == "True")
                RunSeeds(dbContext);
        }

        public static void RunSeeds(CountriesForEveryoneContext dbContext)
        {
            //SeedCountries.Seed(dbContext);
        }
    }
}
