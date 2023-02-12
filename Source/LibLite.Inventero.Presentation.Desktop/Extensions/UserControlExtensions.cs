using LibLite.Inventero.Presentation.Desktop.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace LibLite.Inventero.Presentation.Desktop.Extensions
{
    public static class UserControlExtensions
    {
        public static void InitializeEvents(this ContentControl view)
        {
            view.Loaded += async (object sender, RoutedEventArgs e) =>
            {
                var vm = (ViewModelBase)view.DataContext;
                await vm.OnLoadedAsync();
            };
            view.Unloaded += async (object sender, RoutedEventArgs e) =>
            {
                var vm = (ViewModelBase)view.DataContext;
                await vm.OnUnloadedAsync();
            };
        }
    }
}
