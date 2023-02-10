namespace LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs
{
    public abstract class Input
    {
        public string Label { get; }
        public string Binding { get; }

        protected Input(string label, string binding)
        {
            Label = label;
            Binding = binding;
        }
    }
}
