using CountriesForEveryone.Server.Config.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace CountriesForEveryone.Server.Test.Integration
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<StartupIntegration>>
    {
        protected HttpClient TestClient { get; }
        protected CustomWebApplicationFactory<StartupIntegration> Factory { get; }

        public TestBase(CustomWebApplicationFactory<StartupIntegration> factory)
        {
            Factory = factory;
            TestClient = Factory.CreateClient();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json")
                .Build();
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            SetupAuthenticationHeaderValue(jwtSettings);
        }

        private void SetupAuthenticationHeaderValue(JwtSettings jwtSettings)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}
