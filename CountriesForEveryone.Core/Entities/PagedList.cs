namespace CountriesForEveryone.Core.Entities
{
    public class PagedList<T>
    {
        public PagedList()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }

        public int TotalItemCount { get; set; }
    }
}
