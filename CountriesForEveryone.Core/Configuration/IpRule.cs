namespace CountriesForEveryone.Core.Configuration
{
    public class IpRule
    {
        public string Ip { get; set; }
        public List<RateLimitRule> Rules { get; set; }
    }
}
