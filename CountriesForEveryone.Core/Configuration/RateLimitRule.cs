namespace CountriesForEveryone.Core.Configuration
{
    public class RateLimitRule
    {
        public string Endpoint { get; set; }
        public string Period { get; set; }
        public int Limit { get; set; }
    }
}
