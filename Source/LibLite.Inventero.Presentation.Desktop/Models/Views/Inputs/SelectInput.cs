namespace LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs
{
    public class SelectInput : Input
    {
        public string SearchBinding { get; }
        public string ItemsBinding { get; }
        public string DisplayMember { get; }
        public string SearchCommand { get; }

        public SelectInput(
            string label,
            string binding,
            string searchBinding,
            string itemsBinding,
            string displayMember,
            string searchCommand) : base(label, binding)
        {
            SearchBinding = searchBinding;
            ItemsBinding = itemsBinding;
            DisplayMember = displayMember;
            SearchCommand = searchCommand;
        }
    }
}
