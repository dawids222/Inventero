namespace LibLite.Inventero.Core.Models.Domain
{
    public class Group : Identifiable
    {
        public string Name { get; init; }

        public Group()
        {
            Name = string.Empty;
        }

        public Group(string name)
        {
            Name = name;
        }

        public Group(long id, string name) : this(name)
        {
            Id = id;
        }

        public Group(Group original) : this(
            original.Id,
            original.Name)
        { }
    }
}
