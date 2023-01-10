namespace LibLite.Inventero.Core.Models.Domain
{
    public class Purchase
    {
        public Guid Id { get; }
        public int Amount { get; }
        public double UnitPrice { get; }
        public DateTime Date { get; }
        public Product Product { get; }

        public Purchase(Guid id, int amount, double unitPrice, DateTime date, Product product)
        {
            Id = id;
            Amount = amount;
            UnitPrice = unitPrice;
            Date = date;
            Product = product;
        }
    }
}
