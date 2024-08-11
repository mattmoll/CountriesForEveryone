namespace CountriesForEveryone.Shared
{
    public class PagedResponseDto<T>
    {
        public PagedResponseDto()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }

        public int TotalItemCount { get; set; }
    }
}
