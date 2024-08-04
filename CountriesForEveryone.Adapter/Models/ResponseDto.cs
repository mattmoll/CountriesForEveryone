namespace CountriesForEveryone.Adapter.Models
{
    public class ResponseDto
    {
        public int Status { get; set; }
        public string[] ValidationErrors { get; set; }

        public string Error { get; set; }
    }

    public class ResponseDto<TData> : ResponseDto
    {
        public TData Data { get; set; }
    }
}
