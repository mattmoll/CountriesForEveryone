using System.Net.Http;
using Xunit;

namespace CountriesForEveryone.Server.Test.Integration
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<StartupIntegration>>
    {
        protected HttpClient TestClient { get; }
        protected CustomWebApplicationFactory<StartupIntegration> Factory { get; }

        public TestBase(CustomWebApplicationFactory<StartupIntegration> factory)
        {
            Factory = factory;
            TestClient = Factory.CreateClient();
        }
    }
}
