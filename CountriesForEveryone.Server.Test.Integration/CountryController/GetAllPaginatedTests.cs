using CountriesForEveryone.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Json;

namespace CountriesForEveryone.Server.Test.Integration.CountryController
{
    [Collection("CountriesForEveryone")]
    public class GetAllPaginatedTests : TestBase
    {
        public GetAllPaginatedTests(CustomWebApplicationFactory<StartupIntegration> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllPaginated_ShouldReturnOk()
        {
            // Given
            const HttpStatusCode expectedCode = HttpStatusCode.OK;
            var expectedName = "Argentina";

            var param = new Dictionary<string, string?>
            {
                {"PageNumber", "1"},
                {"PageSize", "10"},
                {"Criteria.Name", expectedName},
                {"SortBy", "Name"},
                {"OrderDirection", "Ascending"}
            };

            // When
            var queryString = QueryHelpers.AddQueryString("/api/countries/", param);

            var response = await TestClient.GetAsync(queryString);
            var result = await response.Content.ReadFromJsonAsync<PagedResponseDto<CountryDetailsDto>>();

            // Then
            response.StatusCode.Should().Be(expectedCode);
            result.Should().NotBeNull();
            result!.TotalItemCount.Should().Be(1);
            result!.Items.First().Name.Should().Be(expectedName);
        }
    }
}
