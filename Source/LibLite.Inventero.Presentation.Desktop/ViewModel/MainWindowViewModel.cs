using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Tools;
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

        private readonly MainMenuViewModel _mainMenuViewModel;

        [ObservableProperty]
        private ObservableObject _menuViewModel;
        [ObservableProperty]
        private ObservableObject _mainViewModel;
        [ObservableProperty]
        private bool _isLoading = false;

        public MainWindowViewModel(
            IEventBus eventBus,
            IViewModelService viewModelService,
            MainMenuViewModel mainMenuViewModel)
        {
            _eventBus = eventBus;
            _viewModelService = viewModelService;

            _mainMenuViewModel = mainMenuViewModel;

            MenuViewModel = _mainMenuViewModel;
            MainViewModel = _viewModelService.Get<PurchasesViewModel>();

            _eventBus.Subscribe<ChangeMainViewEvent>(ChangeMainView);
            _viewModelService = viewModelService;
        }

        private void ChangeMainView(ChangeMainViewEvent @event)
        {
            Dictionary<MainView, ObservableObject> mainViews = new()
            {
                { MainView.Purchases, _viewModelService.Get<PurchasesViewModel>() },
                { MainView.Products, _viewModelService.Get<ProductsViewModel>() },
                { MainView.Groups, _viewModelService.Get<PurchasesViewModel>() },
            };
            var viewModel = mainViews[@event.View];
            MainViewModel = viewModel;
        }
    }
}
