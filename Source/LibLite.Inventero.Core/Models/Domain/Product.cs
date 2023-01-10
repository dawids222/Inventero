namespace LibLite.Inventero.Core.Models.Domain
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }
        public double Price { get; }
        public Group Group { get; }

        public Product(Guid id, string name, double price, Group group)
        {
            Id = id;
            Name = name;
            Price = price;
            Group = group;
        }
    }
}
