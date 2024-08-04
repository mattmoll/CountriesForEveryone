namespace CountriesForEveryone.Core.Validator
{
    public class ValidationError
    {
        public string Message { get; set; }
        public int? InternalErrorCode { get; set; }
        public string ErrorMetadata { get; set; }
        public bool IsBusinessError { get; set; }
    }
}