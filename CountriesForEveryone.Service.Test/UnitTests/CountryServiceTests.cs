using CountriesForEveryone.Core.Adapters;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Exceptions;
using CountriesForEveryone.Core.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace CountriesForEveryone.Service.Test.UnitTests
{
    public class CountryServiceTests
    {
        private readonly CountryService _countryService;
        private readonly Mock<ICountryAdapter> _mockCountryAdapter;
        private readonly Mock<ICountryRepository> _mockCountryRepository;
        private IMemoryCache _memoryCache;

        public CountryServiceTests()
        {
            _mockCountryAdapter = new Mock<ICountryAdapter>(MockBehavior.Strict);
            _mockCountryRepository = new Mock<ICountryRepository>(MockBehavior.Strict);
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _countryService = new CountryService(_mockCountryAdapter.Object, _mockCountryRepository.Object, _memoryCache, Mock.Of<ILogger<CountryService>>());
        }

        [Fact]
        public async Task Get_WhenCodeIsValid_ShouldCallCountryAdapter()
        {
            // Given
            var country = Mocks.Countries.GetOne();
            var countryDetailsExpected = new CountryDetails(country);

            _mockCountryAdapter.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(countryDetailsExpected));

            // When
            var result = await _countryService.Get(country.Alpha2Code);

            // Then
            result.Alpha2Code.Should().Be(countryDetailsExpected.Alpha2Code);
            result.Name.Should().Be(countryDetailsExpected.Name);
            _mockCountryAdapter.Verify(c => c.Get(countryDetailsExpected.Alpha2Code), Times.Once());
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

        [Fact]
        public async Task Get_WhenSecondCallToGetSameCountry_ShouldNotCallCountryAdapterAndUseMemoryCache()
        {
            // Given
            var country = Mocks.Countries.GetOne();
            var countryDetailsExpected = new CountryDetails(country);
            var countryMemoryCacheKey = $"CountryDetails-{countryDetailsExpected.Alpha2Code}";

            _mockCountryAdapter.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(countryDetailsExpected));

            // When
            var result = await _countryService.Get(country.Alpha2Code);
            
            var resultSecondCall = await _countryService.Get(country.Alpha2Code);

            // Then, Only once the adapter should be called, the second time it uses the MemoryCache
            _mockCountryAdapter.Verify(c => c.Get(countryDetailsExpected.Alpha2Code), Times.Once());
            _memoryCache.TryGetValue(countryMemoryCacheKey, out CountryDetails? countryInCache);
            countryInCache.Should().NotBeNull();
            countryInCache!.Alpha2Code.Should().Be(countryDetailsExpected.Alpha2Code);
        }
    }
}
