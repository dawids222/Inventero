namespace LibLite.Inventero.Presentation.Desktop.Models.Views
{
    public class Column
    {
        public string Header { get; }
        public string Property { get; }

        public Column(string header, string property)
        {
            Header = header;
            Property = property;
        }
    }
}
