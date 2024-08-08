using CountriesForEveryone.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesForEveryone.Repository.Engines
{
    public class InMemoryEngine : IEngine
    {
        public void AddDbContext(string connectionString, IServiceCollection services)
        {
            var dataBaseName = $"CountriesForEveryoneInMemoryDb{DateTime.Now.Ticks}{new Random().Next()}";
            services.AddDbContext<CountriesForEveryoneContext>(opt => opt.UseInMemoryDatabase(dataBaseName));
        }
    }
}
