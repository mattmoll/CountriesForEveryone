using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CountriesForEveryone.Repository.Contexts
{
    public class CountriesForEveryoneContextFactory : IDesignTimeDbContextFactory<CountriesForEveryoneContext>
    {
        public CountriesForEveryoneContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CountriesForEveryoneContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CountriesForEveryoneConnectionString");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new CountriesForEveryoneContext(optionsBuilder.Options);
        }
    }
}