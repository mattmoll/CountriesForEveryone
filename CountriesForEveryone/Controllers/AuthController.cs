using CountriesForEveryone.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountriesForEveryone.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ICountriesAuthenticationService _authenticationService;

        public AuthController(ICountriesAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            // Here we could add some validations for user credentials or anything we want.
            var token = _authenticationService.GenerateJwtToken("guest");
            return Ok(new { Token = token });
        }
    }
}