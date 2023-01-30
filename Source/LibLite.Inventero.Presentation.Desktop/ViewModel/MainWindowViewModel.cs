using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Events;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IEventBus _eventBus;
        private readonly IViewModelService _viewModelService;

        private readonly IProductStore _productStore;
        private readonly IGroupStore _groupStore;

        [ObservableProperty]
        private ObservableObject _menuViewModel;
        [ObservableProperty]
        private ObservableObject _mainViewModel;
        [ObservableProperty]
        private bool _isLoading = false;

        public MainWindowViewModel(
            IEventBus eventBus,
            IViewModelService viewModelService,
            IProductStore productStore,
            IGroupStore groupStore)
        {
            _eventBus = eventBus;
            _viewModelService = viewModelService;

            _eventBus.Subscribe<ChangeMainViewEvent>(ChangeMainView);
            _productStore = productStore;
            _groupStore = groupStore;
        }

        private void ChangeMainView(ChangeMainViewEvent @event)
        {
            Dictionary<MainView, ObservableObject> mainViews = new()
            {
                { MainView.Purchases, _viewModelService.Get<PurchasesViewModel>() },
                { MainView.Products, _viewModelService.Get<ProductsViewModel>() },
                { MainView.Groups, _viewModelService.Get<GroupsViewModel>() },
                { MainView.Product, _viewModelService.Get<ProductViewModel>() },
            };
            var viewModel = mainViews[@event.View];
            MainViewModel = viewModel;
        }

        [RelayCommand]
        private async void LoadViews()
        {
            var groups = await _groupStore.GetAsync(new PaginatedListRequest());
            var product = new Product("Test", 3.24, new Group(default, "Test"));
            await _productStore.StoreAsync(product);

            MenuViewModel = _viewModelService.Get<MainMenuViewModel>();
            MainViewModel = _viewModelService.Get<PurchasesViewModel>();
        }
    }
}
