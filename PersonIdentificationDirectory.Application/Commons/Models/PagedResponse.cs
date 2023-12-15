namespace PersonIdentificationDirectory.Application.Commons.Models
{
    public record PagedResponse
    {
        public int? Page { get; init; }
        public int? PageSize { get; init; }
        public int TotalCount { get; set; }
        public PagedResponse(int? page, int? pageSize, int totalCount)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
