namespace Application.Common.Models
{
    public class PaginatedList<TData>
    {
        public IReadOnlyList<TData> Data { get; private set; }

        public int TotalPages { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        private PaginatedList() { }

        public PaginatedList(IReadOnlyList<TData> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data;
            TotalPages = (int)Math.Ceiling(totalCount / (double) pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
