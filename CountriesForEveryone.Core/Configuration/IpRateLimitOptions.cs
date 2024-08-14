namespace CountriesForEveryone.Core.Configuration
{
    public class IpRateLimitOptions
    {
        public bool EnableEndpointRateLimiting { get; set; }
        public bool StackBlockedRequests { get; set; }
        public string RealIpHeader { get; set; }
        public string ClientIdHeader { get; set; }
        public int HttpStatusCode { get; set; }
        public List<RateLimitRule> GeneralRules { get; set; }
    }
}
