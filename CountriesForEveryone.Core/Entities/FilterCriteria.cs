namespace CountriesForEveryone.Core.Entities
{
    public class FilterCriteria<TCriteria>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public TCriteria Criteria { get; set; }
        public string SortBy { get; set; }
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Ascending;
    }
}