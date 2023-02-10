using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel.Items
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IViewModelService _viewModelService;

        [ObservableProperty]
        private ViewModelBase _menuViewModel;
        [ObservableProperty]
        private ViewModelBase _contentViewModel;
        [ObservableProperty]
        private bool _isLoading = false;

        public MainWindowViewModel(IViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
        }

        protected override Task LoadAsync()
        {
            MenuViewModel = _viewModelService.Get<MainMenuViewModel>();
            ContentViewModel = _viewModelService.Get<PurchasesViewModel>();
            return Task.CompletedTask;
        }
    }
}
