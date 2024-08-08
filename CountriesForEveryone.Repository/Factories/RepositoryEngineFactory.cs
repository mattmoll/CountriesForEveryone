using CountriesForEveryone.Repository.Engines;

namespace CountriesForEveryone.Repository.Factories
{
    public static class RepositoryEngineFactory
    {
        private const string MySql = "mysql";
        private const string InMemory = "inmemory";

        private static readonly IDictionary<string, Func<IEngine>> EngineMap =
            new Dictionary<string, Func<IEngine>>()
            {
                {MySql, () => new MySqlEngine()},
                {InMemory, () => new InMemoryEngine()}
            };

        public static IEngine CreateEngineFromConnectionName(string connectionName)
        {
            return EngineMap[connectionName.ToLowerInvariant()]();
        }
    }
}
