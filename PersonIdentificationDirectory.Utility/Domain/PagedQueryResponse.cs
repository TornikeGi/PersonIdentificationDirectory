namespace PersonIdentificationDirectory.Utility.Domain
{
    public sealed class PagedQueryResponse<T>
    {
        public PagedQueryResponse(
            int page, 
            int pageSize,
            List<T> data,
            int totalCount)
        {
            Page = page;
            PageSize = pageSize;
            Data = data;
            TotalCount = totalCount;
        }

        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }
}
