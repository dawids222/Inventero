using System.Windows.Input;

namespace LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs
{
    public class SelectInput : Input
    {
        public string SearchBinding { get; set; }
        public string SelectedItemBinding { get; set; }
        public string ItemsBinding { get; set; }
        public string DisplayMember { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public SelectInput(
            string label,
            string binding,
            string searchBinding,
            string selectedItemBinding,
            string itemsBinding,
            string displayMember,
            ICommand searchCommand,
            ICommand selectionChangedCommand) : base(label, binding)
        {
            SearchBinding = searchBinding;
            SelectedItemBinding = selectedItemBinding;
            ItemsBinding = itemsBinding;
            DisplayMember = displayMember;
            SearchCommand = searchCommand;
            SelectionChangedCommand = selectionChangedCommand;
        }
    }
}
