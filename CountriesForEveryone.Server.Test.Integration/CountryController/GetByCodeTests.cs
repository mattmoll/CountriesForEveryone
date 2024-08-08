using CountriesForEveryone.Shared;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace CountriesForEveryone.Server.Test.Integration.CountryController
{
    [Collection("CountriesForEveryone")]
    public class GetByCodeTests : TestBase
    {
        public GetByCodeTests(CustomWebApplicationFactory<StartupIntegration> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            // Given
            const HttpStatusCode expectedCode = HttpStatusCode.OK;
            var countryCode = "AR";
            var expectedName = "Argentina";

            // When
            var response = await TestClient.GetAsync($"/api/countries/{countryCode}");
            var result = await response.Content.ReadFromJsonAsync<CountryDto>();

            // Then
            response.StatusCode.Should().Be(expectedCode);
            result.Should().NotBeNull();
            result!.Alpha2Code.Should().Be(countryCode);
            result!.Name.Should().Be(expectedName);
        }
    }
}
