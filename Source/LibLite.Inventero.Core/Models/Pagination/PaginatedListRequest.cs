namespace LibLite.Inventero.Core.Models.Pagination
{
    public class PaginatedListRequest
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public string Search { get; init; }
        public IEnumerable<Sort> Sorts { get; init; }

        public PaginatedListRequest(int pageIndex, int pageSize, string search, IEnumerable<Sort> sorts)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Search = search;
            Sorts = sorts;
        }

        public PaginatedListRequest(PaginatedListRequest original)
            : this(original.PageIndex, original.PageSize, original.Search, original.Sorts) { }

        public PaginatedListRequest(int pageIndex, int pageSize, IEnumerable<Sort> sorts)
            : this(pageIndex, pageSize, string.Empty, sorts) { }

        public PaginatedListRequest(int pageIndex, int pageSize, string search)
            : this(pageIndex, pageSize, search, new List<Sort>()) { }

        public PaginatedListRequest(int pageIndex, int pageSize)
            : this(pageIndex, pageSize, string.Empty, new List<Sort>()) { }

        public PaginatedListRequest()
            : this(0, 20, string.Empty, new List<Sort>()) { }
    }
}
