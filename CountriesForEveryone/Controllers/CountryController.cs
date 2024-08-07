using AutoMapper;
using CountriesForEveryone.Core.Services;
using CountriesForEveryone.Shared;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CountriesForEveryone.Server.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("{countryCode}")]
        public async Task<ActionResult<CountryDto>> GetByCode([Required] string countryCode)
        {
            if (countryCode.Length < 2 || countryCode.Length > 3)
                return BadRequest("Country Code must be alpha code 2 letters, alpha code 3 letters or ISO 3166-1 numeric code (3 numbers)");

            var country = await _countryService.Get(countryCode);
            return Ok(_mapper.Map<CountryDto>(country));
        }
    }
}