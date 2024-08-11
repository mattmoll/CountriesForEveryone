namespace CountriesForEveryone.Shared
{
    public class PagingRequestDto<TCriteria>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public TCriteria Criteria { get; set; }
        public string? SortBy { get; set; }
        public OrderDirectionDto? OrderDirection { get; set; } = OrderDirectionDto.Ascending;
    }
}
