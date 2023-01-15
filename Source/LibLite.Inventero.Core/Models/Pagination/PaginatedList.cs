using System.Collections;

namespace LibLite.Inventero.Core.Models.Pagination
{
    public class PaginatedList<T> : IEnumerable<T>
    {
        public int PageSize { get; }
        public int PageIndex { get; }
        public int TotalItems { get; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasNextPage => PageIndex < TotalPages - 1;
        public bool HasPreviousPage => PageIndex > 0;

        public IEnumerable<T> Items { get; }

        public PaginatedList(
            IEnumerable<T> items,
            int pageIndex,
            int pageSize,
            int totalItems)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public PaginatedList<TCast> Cast<TCast>()
        {
            var items = Items.Cast<TCast>().ToList();
            return new PaginatedList<TCast>(items, PageSize, PageIndex, TotalItems);
        }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
