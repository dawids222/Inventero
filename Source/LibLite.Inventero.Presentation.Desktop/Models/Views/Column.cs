namespace LibLite.Inventero.Presentation.Desktop.Models.Views
{
    public class Column
    {
        public string Header { get; }
        public string Property { get; }
        public string StringFormat { get; }

        public Column(string header, string property, string stringFormat = null)
        {
            Header = header;
            Property = property;
            StringFormat = stringFormat;
        }
    }
}
