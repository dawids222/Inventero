using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Presentation.Desktop.Interfaces;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainMenuViewModel : ObservableObject
    {
        private readonly IViewService _viewService;

        public MainMenuViewModel(IViewService viewService)
        {
            _viewService = viewService;
        }

        [RelayCommand]
        private void ShowPurchasesView() => _viewService.ShowPurchasesView();
        [RelayCommand]
        private void ShowProductsView() => _viewService.ShowProductsView();
        [RelayCommand]
        private void ShowGroupsView() => _viewService.ShowGroupsView();
        [RelayCommand]
        private void ShowInventoryView() { }
    }
}
