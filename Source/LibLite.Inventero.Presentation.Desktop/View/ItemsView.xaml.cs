using LibLite.Inventero.Presentation.Desktop.Extensions;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.View
{
    /// <summary>
    /// Interaction logic for PaginatedListView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        public ItemsView()
        {
            InitializeComponent();
            this.InitializeEvents();
            Loaded += PaginatedListView_Loaded;
        }

        private void PaginatedListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateColumns();
            MoveActionsToTheEnd();
        }

        private void GenerateColumns()
        {
            var viewModel = (ItemsViewModel)DataContext;
            var columns = viewModel.Columns;
            foreach (var column in columns)
            {
                ItemsDataGrid.Columns.Add(CreateTextColumn(column));
            }
        }

        private static DataGridTextColumn CreateTextColumn(Column column)
        {
            var binding = CreateTextColumnBinding(column);
            return new DataGridTextColumn
            {
                Header = column.Header,
                Binding = binding,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 16,
                IsReadOnly = true,
                CanUserSort = false,
            };
        }

        private static Binding CreateTextColumnBinding(Column column)
        {
            var binding = new Binding(column.Property);
            if (!string.IsNullOrEmpty(column.StringFormat))
            {
                binding.StringFormat = column.StringFormat;
            }
            return binding;
        }

        private void MoveActionsToTheEnd()
        {
            var actionsColumn = ItemsDataGrid.Columns.First();
            ItemsDataGrid.Columns.Remove(actionsColumn);
            ItemsDataGrid.Columns.Add(actionsColumn);
        }
    }
}
