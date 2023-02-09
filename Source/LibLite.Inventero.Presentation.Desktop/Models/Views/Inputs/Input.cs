namespace LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs
{
    public abstract class Input
    {
        public string Label { get; set; }
        public string Binding { get; set; }

        protected Input(string label, string binding)
        {
            Label = label;
            Binding = binding;
        }
    }
}
