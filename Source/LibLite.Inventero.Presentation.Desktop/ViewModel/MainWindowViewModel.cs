using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        [ObservableProperty]
        private ObservableObject _menuViewModel;
        [ObservableProperty]
        private ObservableObject _mainViewModel;
        [ObservableProperty]
        private bool _isLoading = false;

        public MainWindowViewModel(
            IEventBus eventBus,
            IViewModelService viewModelService)
        {
            _eventBus = eventBus;
            _viewModelService = viewModelService;

            _eventBus.Subscribe<ChangeMainViewEvent>(ChangeMainView);
        }

        private void ChangeMainView(ChangeMainViewEvent @event)
        {
            Dictionary<MainView, ObservableObject> mainViews = new()
            {
                { MainView.Purchases, _viewModelService.Get<PurchasesViewModel>() },
                { MainView.Products, _viewModelService.Get<ProductsViewModel>() },
                { MainView.Groups, _viewModelService.Get<GroupsViewModel>() },
            };
            var viewModel = mainViews[@event.View];
            MainViewModel = viewModel;
        }

        [RelayCommand]
        private void LoadViews()
        {
            MenuViewModel = _viewModelService.Get<MainMenuViewModel>();
            MainViewModel = _viewModelService.Get<PurchasesViewModel>();
        }
    }
}
