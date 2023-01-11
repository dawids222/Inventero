namespace LibLite.Inventero.DAL.Entities
{
    public class PurchaseEntity : Entity
    {
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
        public DateTime Date { get; set; }
        public ProductEntity Product { get; set; }
    }
}
