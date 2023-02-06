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
            Amount = 0;
            UnitPrice = 0;
            Date = DateTime.MinValue;
            Product = null;
        }

        public Purchase(int amount, double unitPrice, DateTime date, Product product)
        {
            Amount = amount;
            UnitPrice = unitPrice;
            Date = date;
            Product = product;
        }

        public Purchase(long id, int amount, double unitPrice, DateTime date, Product product)
            : this(amount, unitPrice, date, product)
        {
            Id = id;
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
