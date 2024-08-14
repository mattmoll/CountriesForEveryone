using CountriesForEveryone.Server.Config.Models;
using CountriesForEveryone.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace CountriesForEveryone.Server.Test.Integration.LanguageController
{
    [Collection("CountriesForEveryone")]
    public class GetAllByCriteriaTests : TestBase
    {
        public GetAllByCriteriaTests(CustomWebApplicationFactory<StartupIntegration> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllByCriteria_ShouldReturnOk()
        {
            // Given
            const HttpStatusCode expectedCode = HttpStatusCode.OK;
            const string expectedLanguageName = "Spanish";

            var param = new Dictionary<string, string?>
            {
                {"LanguageName", expectedLanguageName},
                {"LanguageCode", null},
            };

            // When
            var queryString = QueryHelpers.AddQueryString("/api/languages/", param);

            var response = await TestClient.GetAsync(queryString);
            var result = await response.Content.ReadFromJsonAsync<List<LanguageWithCountriesDto>>();

            // Then
            response.StatusCode.Should().Be(expectedCode);
            result.Should().NotBeNull();
            result!.Count().Should().Be(1);
            result!.First().Name.Should().Be(expectedLanguageName);
        }
    }
}
