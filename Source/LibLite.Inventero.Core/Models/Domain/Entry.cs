namespace LibLite.Inventero.Core.Models.Domain
{
    public class Entry
    {
        public int Amount { get; }
        public Product Product { get; }

        public double Value => Product.Price * Amount;

        public Entry(int amount, Product product)
        {
            Amount = amount;
            Product = product;
        }
    }
}
