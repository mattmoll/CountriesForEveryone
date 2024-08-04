namespace CountriesForEveryone.Adapter.Models
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new();
        }
        public List<string> Errors { get; set; }
    }
}
