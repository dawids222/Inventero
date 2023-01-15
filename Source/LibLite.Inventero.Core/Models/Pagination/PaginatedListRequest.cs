namespace LibLite.Inventero.Core.Models.Pagination
{
    public class PaginatedListRequest
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public IEnumerable<Sort> Sorts { get; }

        public PaginatedListRequest(int pageIndex, int pageSize, IEnumerable<Sort> sorts)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Sorts = sorts;
        }

        public PaginatedListRequest(int pageIndex, int pageSize)
            : this(pageIndex, pageSize, new List<Sort>()) { }

        public PaginatedListRequest()
            : this(0, 20, new List<Sort>()) { }
    }
}
