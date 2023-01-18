namespace LibLite.Inventero.Core.Models.Pagination
{
    public class PaginatedListRequest
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public string Search { get; set; }
        public IEnumerable<Sort> Sorts { get; }

        public PaginatedListRequest(int pageIndex, int pageSize, string search, IEnumerable<Sort> sorts)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Search = search;
            Sorts = sorts;
        }

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
