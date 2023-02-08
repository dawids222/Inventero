using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Presentation.Desktop.Interfaces;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainMenuViewModel : ViewModelBase
    {
        private readonly IViewService _viewService;

        public MainMenuViewModel(IViewService viewService)
        {
            _viewService = viewService;
        }

        [RelayCommand]
        private void ShowPurchasesView() => _viewService.ShowPurchases();
        [RelayCommand]
        private void ShowProductsView() => _viewService.ShowProducts();
        [RelayCommand]
        private void ShowGroupsView() => _viewService.ShowGroups();
        [RelayCommand]
        private void ShowInventoryView() { }
    }
}
