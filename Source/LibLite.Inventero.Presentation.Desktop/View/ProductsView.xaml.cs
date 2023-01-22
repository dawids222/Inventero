using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.View
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();

            DataContextChanged += ProductsView_DataContextChanged;
        }

        private void ProductsView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
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

        private DataGridTextColumn CreateTextColumn(Column column)
        {
            return new DataGridTextColumn
            {
                Header = column.Header,
                Binding = new Binding(column.Property),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 16,
                IsReadOnly = true,
                CanUserSort = false,
            };
        }
    }
}
