using AutoMapper;
using CountriesForEveryone.Core.Exceptions;
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
            try
            {
                var country = await _countryService.Get(countryCode);

                return Ok(_mapper.Map<CountryDto>(country));

            }
            catch (InvalidParameterException ex)
            {
                return ValidationProblem(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode:500);
            }
        }
    }
}