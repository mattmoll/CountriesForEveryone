namespace CountriesForEveryone.Core.Entities
{
    public class Region
    {
        public Region(string name) 
        { 
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
    }
}
