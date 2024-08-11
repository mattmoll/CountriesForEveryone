using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CountriesForEveryone.Service.Test.UnitTests
{
    public class CountryServiceTests
    {
        private readonly CountryService _countryService;
        private readonly Mock<ICountryAdapter> _mockCountryAdapter;

        public CountryServiceTests()
        {
            _mockCountryAdapter = new Mock<ICountryAdapter>(MockBehavior.Strict);
            _countryService = new CountryService(_mockCountryAdapter.Object, Mock.Of<ILogger<CountryService>>());
        }

        [Fact]
        public async Task Get_WhenCodeIsValid_ShouldCallCountryAdapter()
        {
            // Given
            var country = Mocks.Countries.GetOne();

            _mockCountryAdapter.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new CountryDetails(country)));

            // When
            var result = await _countryService.Get(country.Alpha2Code);

            // Then
            result.Alpha2Code.Should().Be(country.Alpha2Code);
            result.Name.Should().Be(country.Name);
            _mockCountryAdapter.Verify(c => c.Get(country.Alpha2Code), Times.Once());
        }

        [Fact]
        public async Task Get_WhenCodeIsNOTValid_ShouldThrowException()
        {
            // Given
            var country = new CountryDetails(Mocks.Countries.GetOne());
            country.Alpha2Code = "InvalidCodeToSearchFor";

            _mockCountryAdapter.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(country));

            // When
            await _countryService.Invoking(c => c.Get(country.Alpha2Code))
            // Then
                .Should()
                .ThrowAsync<InvalidParameterException>()
                .WithMessage("Country Code must be alpha code 2 letters, alpha code 3 letters or ISO 3166-1 numeric code (3 numbers)");
        }
    }
}
