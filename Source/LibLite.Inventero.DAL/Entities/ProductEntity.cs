namespace LibLite.Inventero.DAL.Entities
{
    public class ProductEntity : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public GroupEntity Group { get; set; }
    }
}
