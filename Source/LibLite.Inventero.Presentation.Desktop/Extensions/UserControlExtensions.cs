using LibLite.Inventero.Presentation.Desktop.ViewModel;
using System.Windows.Controls;

namespace LibLite.Inventero.Presentation.Desktop.Extensions
{
    public static class UserControlExtensions
    {
        public static void InitializeEvents(this ContentControl view)
        {
            view.Loaded += delegate
            {
                var vm = (ViewModelBase)view.DataContext;
                vm.OnLoaded();
            };
            view.Unloaded += delegate
            {
                var vm = (ViewModelBase)view.DataContext;
                vm.OnUnloaded();
            };
        }
    }
}
