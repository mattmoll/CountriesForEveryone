namespace CountriesForEveryone.Adapter.Models
{
    public class ValidationErrorDto
    {
        public string Message { get; set; }
        public int? InternalErrorCode { get; set; }
        public bool IsBusinessError { get; set; }
    }
}
