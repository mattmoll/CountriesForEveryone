namespace CountriesForEveryone.Core.Entities
{
    public class Language
    {
        public Language() { }

        public Language(string code, string name) 
        {
            Id = Guid.Empty;
            Code = code;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
    }
}
