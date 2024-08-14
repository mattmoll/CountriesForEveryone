using CountriesForEveryone.Server.Config.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Net;

namespace CountriesForEveryone.Server.Test.Integration.General
{
    public class RateLimitTests : TestBase
    {
        private const int RATE_LIMIT = 30;
        public RateLimitTests(CustomWebApplicationFactory<StartupIntegration> factory) : base(factory)
        {
        }

        [Fact]
        public async Task RateLimiting_AllowedUnderLimitAndBlockedAboveLimit()
        {
            const string countryCode = "AR";
            HttpStatusCode allowedCode = HttpStatusCode.OK;
            HttpStatusCode blockedCode = HttpStatusCode.TooManyRequests;

            for (int i = 0; i < RATE_LIMIT; i++)
            {
                var response = await TestClient.GetAsync($"/api/countries/{countryCode}");
                response.StatusCode.Should().Be(allowedCode);
            }

            var blockedResponse = await TestClient.GetAsync($"/api/countries/{countryCode}");
            blockedResponse.StatusCode.Should().Be(blockedCode);
        }
    }
}
