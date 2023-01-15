namespace LibLite.Inventero.Core.Models.Pagination
{
    public class Sort
    {
        public string Property { get; init; }
        public string Direction { get; init; }

        public Sort() : this(string.Empty, string.Empty) { }
        public Sort(string property, string direction)
        {
            Property = property;
            Direction = direction;
        }
    }
}
