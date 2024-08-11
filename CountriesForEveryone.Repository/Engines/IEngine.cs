using Microsoft.Extensions.DependencyInjection;

namespace CountriesForEveryone.Repository.Engines
{
    public interface IEngine
    {
        void AddDbContext(string connectionString, IServiceCollection services);
    }
}
