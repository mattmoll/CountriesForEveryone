using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CountriesForEveryone.Repository.Contexts
{
    public class CountriesForEveryoneContextFactory : IDesignTimeDbContextFactory<CountriesForEveryoneContext>
    {
        public CountriesForEveryoneContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CountriesForEveryoneContext>();
            var connectionString =
                "server=localhost;port=3306;database=CountriesForEveryone;user=root;password=root;Persist Security Info=False; Connect Timeout=300";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new CountriesForEveryoneContext(optionsBuilder.Options);
        }
    }
}