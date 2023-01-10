namespace LibLite.Inventero.Core.Models.Domain
{
    public class Group
    {
        public Guid Id { get; }
        public string Name { get; }

        public Group(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
