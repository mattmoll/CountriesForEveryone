using Microsoft.EntityFrameworkCore;
using CountriesForEveryone.Core.Entities;
using System.Reflection;

namespace CountriesForEveryone.Repository.Contexts
{
    public class CountriesForEveryoneContext : DbContext
    {
        public CountriesForEveryoneContext(DbContextOptions<CountriesForEveryoneContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyUtcDateTimeConverter();
        }
    }
}
