using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CountriesForEveryone.Repository.Contexts;

namespace CountriesForEveryone.Repository.Engines
{
    public class MySqlEngine : IEngine
    {
        public void AddDbContext(string connectionString, IServiceCollection services)
        {
            services.AddDbContext<CountriesForEveryoneContext>(opt =>
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
