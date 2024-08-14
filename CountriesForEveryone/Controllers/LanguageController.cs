using AutoMapper;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Services;
using CountriesForEveryone.Shared;
using CountriesForEveryone.Shared.Criteria;
using Microsoft.AspNetCore.Mvc;

namespace CountriesForEveryone.Server.Controllers
{
    [ApiController]
    [Route("api/languages")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly ILogger<LanguageController> _logger;

        public LanguageController(ILanguageService languageService, IMapper mapper, ILogger<LanguageController> logger)
        {
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageWithCountriesDto>>> GetAllByCriteria([FromQuery] LanguageCriteriaDto languageCriteria)
        {
            try
            {
                var filterCriteria = _mapper.Map<LanguageCriteria>(languageCriteria);

                var languagesWithCountries = await _languageService.GetAllByCriteria(filterCriteria);

                return Ok(_mapper.Map<List<LanguageWithCountriesDto>>(languagesWithCountries));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}