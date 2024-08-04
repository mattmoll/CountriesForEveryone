namespace CountriesForEveryone.Core.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message) : base(message) { }
        public ExternalApiException(string message, Exception ex) : base(message, ex) { }
    }
}
