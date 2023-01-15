namespace LibLite.Inventero.Core.Models.Domain
{
    public class Purchase : Identifiable
    {
        public int Amount { get; init; }
        public double UnitPrice { get; init; }
        public DateTime Date { get; init; }
        public Product Product { get; init; }

        public Purchase()
        {
            Id = Guid.Empty;
            Amount = 0;
            UnitPrice = 0;
            Date = DateTime.MinValue;
            Product = null;
        }

        public Purchase(Guid id, int amount, double unitPrice, DateTime date, Product product)
        {
            Id = id;
            Amount = amount;
            UnitPrice = unitPrice;
            Date = date;
            Product = product;
        }

        public Purchase(Purchase original) : this(
            original.Id,
            original.Amount,
            original.UnitPrice,
            original.Date,
            original.Product)
        { }
    }
}
