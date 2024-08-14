namespace CountriesForEveryone.Core.Services
{
    public interface ICountriesAuthenticationService
    {
        string GenerateJwtToken(string username);
    }
}
