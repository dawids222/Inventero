using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
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

        protected override async Task LoadAsync()
        {
            MenuViewModel = _viewModelService.Get<MainMenuViewModel>();
            var contentViewModel = _viewModelService.Get<PurchasesViewModel>();
            ContentViewModel = contentViewModel;
            await contentViewModel.LoadItemsCommand.ExecuteAsync(null);
        }
    }
}
