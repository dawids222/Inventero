namespace LibLite.Inventero.Core.Models.Domain
{
    public class Inventory
    {
        public string Name { get; }
        IEnumerable<Entry> Entries { get; }

        public double Value => Entries.Sum(x => x.Value);

        public Inventory(string name, IEnumerable<Entry> entries)
        {
            Name = name;
            Entries = entries;
        }
    }
}
