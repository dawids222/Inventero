namespace LibLite.Inventero.Core.Models.Domain
{
    public class Product : Identifiable
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public Group Group { get; init; }

        public Product()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Price = 0;
            Group = null;
        }

        public Product(Guid id, string name, double price, Group group)
        {
            Id = id;
            Name = name;
            Price = price;
            Group = group;
        }

        public Product(Product original) : this(
            original.Id,
            original.Name,
            original.Price,
            original.Group)
        { }
    }
}
