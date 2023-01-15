namespace LibLite.Inventero.Core.Models.Domain
{
    public class Group : Identifiable
    {
        public string Name { get; init; }

        public Group()
        {
            Id = Guid.Empty;
            Name = string.Empty;
        }

        public Group(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Group(Group original) : this(
            original.Id,
            original.Name)
        { }
    }
}
