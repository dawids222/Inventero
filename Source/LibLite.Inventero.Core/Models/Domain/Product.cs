namespace LibLite.Inventero.Core.Models.Domain
{
    public class Product : Identifiable
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public Group Group { get; init; }

        public Product()
        {
            Name = string.Empty;
            Price = 0;
            Group = null;
        }

        public Product(string name, double price, Group group)
        {
            Name = name;
            Price = price;
            Group = group;
        }

        public Product(long id, string name, double price, Group group)
            : this(name, price, group)
        {
            Id = id;
        }

        public Product(Product original) : this(
            original.Id,
            original.Name,
            original.Price,
            original.Group)
        { }
    }
}
