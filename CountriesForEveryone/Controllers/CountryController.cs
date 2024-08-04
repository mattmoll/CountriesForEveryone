using CountriesForEveryone.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountriesForEveryone.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}