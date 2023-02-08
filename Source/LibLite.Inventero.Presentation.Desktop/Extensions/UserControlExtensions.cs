using LibLite.Inventero.Presentation.Desktop.ViewModel;
using System.Windows.Controls;

namespace LibLite.Inventero.Presentation.Desktop.Extensions
{
    public static class UserControlExtensions
    {
        public static void InitializeEvents(this ContentControl view)
        {
            view.DataContextChanged += delegate
            {
                var vm = (ViewModelBase)view.DataContext;
                view.Loaded += delegate { vm.OnLoaded(); };
                view.Unloaded += delegate { vm.OnUnloaded(); };
            };
        }
    }
}
