namespace CountriesForEveryone.Shared
{
    public class NameDto
    {
        public string Common { get; set; }
        public string Official { get; set; }
        public Dictionary<string, NativeNameDto> NativeName { get; set; }
    }
}
