using AutoMapper;
using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Services;
using CountriesForEveryone.Shared;
using CountriesForEveryone.Shared.Criteria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountriesForEveryone.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/regions")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionController> _logger;

        public RegionController(IRegionService regionService, IMapper mapper, ILogger<RegionController> logger)
        {
            _regionService = regionService ?? throw new ArgumentNullException(nameof(regionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<List<RegionWithCountriesDto>>> GetAllByCriteria([FromQuery] RegionCriteriaDto regionCriteria)
        {
            try
            {
                var filterCriteria = _mapper.Map<RegionCriteria>(regionCriteria);

                var regionsWithCountries = await _regionService.GetAllByCriteria(filterCriteria);

                return Ok(_mapper.Map<List<RegionWithCountriesDto>>(regionsWithCountries));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}