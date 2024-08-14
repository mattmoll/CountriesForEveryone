using CountriesForEveryone.Server.Config.Models;
using CountriesForEveryone.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CountriesForEveryone.Server.Test.Integration.RegionController
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
            const string expectedRegionName = "America";

            var param = new Dictionary<string, string?>
            {
                {"RegionName", expectedRegionName},
                {"CountriesUnitedNationsMember", null},
                {"CountryName", null}
            };

            // When
            var queryString = QueryHelpers.AddQueryString("/api/regions/", param);

            var response = await TestClient.GetAsync(queryString);
            var result = await response.Content.ReadFromJsonAsync<List<RegionWithCountriesDto>>();

            // Then
            response.StatusCode.Should().Be(expectedCode);
            result.Should().NotBeNull();
            result!.Count().Should().Be(1);
            result!.First().Name.Should().Be(expectedRegionName);
        }
    }
}
