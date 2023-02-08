using LibLite.Inventero.Presentation.Desktop.Extensions;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.View
{
    /// <summary>
    /// Interaction logic for PaginatedListView.xaml
    /// </summary>
    public partial class PaginatedListView : UserControl
    {
        public PaginatedListView()
        {
            InitializeComponent();
            this.InitializeEvents();
            Loaded += PaginatedListView_Loaded;
        }

        private void PaginatedListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var columns = (DataContext as dynamic).Columns as IEnumerable<Column>;
            foreach (var column in columns)
            {
                ItemsDataGrid.Columns.Add(CreateTextColumn(column));
            }
            var firstColumn = ItemsDataGrid.Columns.First();
            ItemsDataGrid.Columns.Remove(firstColumn);
            ItemsDataGrid.Columns.Add(firstColumn);
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
    }
}
