using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Presentation.Desktop.Interfaces;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IViewModelService _viewModelService;

        [ObservableProperty]
        private ObservableObject _menuViewModel;
        [ObservableProperty]
        private ObservableObject _mainViewModel;
        [ObservableProperty]
        private bool _isLoading = false;

        public MainWindowViewModel(IViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
        }

        [RelayCommand]
        private void LoadViews()
        {
            MenuViewModel = _viewModelService.Get<MainMenuViewModel>();
            MainViewModel = _viewModelService.Get<PurchasesViewModel>();
        }
    }
}
