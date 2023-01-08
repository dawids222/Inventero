using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly Test1ViewModel _test1ViewModel;
        private readonly Test2ViewModel _test2ViewModel;

        [ObservableProperty]
        private ObservableObject? _view;

        [ObservableProperty]
        private string _name = "Ania";

        public MainWindowViewModel(
            Test1ViewModel test1ViewModel,
            Test2ViewModel test2ViewModel)
        {
            _test1ViewModel = test1ViewModel;
            _test2ViewModel = test2ViewModel;

            View = _test1ViewModel;
        }

        [RelayCommand]
        private void ChangeName()
        {
            Name = string.Join("", _name.Reverse());
        }

        [RelayCommand]
        private void ChangeView()
        {
            if (View is Test1ViewModel)
                View = _test2ViewModel;
            else
                View = _test1ViewModel;
        }
    }
}
