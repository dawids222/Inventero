using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Events;
using System;
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
            Dictionary<MainView, Func<ObservableObject>> mainViews = new()
            {
                { MainView.Purchases,  () =>
                    {
                        var viewModel = _viewModelService.Get<PurchasesViewModel>();
                        viewModel.LoadItemsCommand.Execute(null);
                        return viewModel;
                    }
                },
                { MainView.Products, () =>
                    {
                        var viewModel = _viewModelService.Get<ProductsViewModel>();
                        viewModel.LoadItemsCommand.Execute(null);
                        return viewModel;
                    }
                },
                { MainView.Groups, () =>
                    {
                        var viewModel = _viewModelService.Get<GroupsViewModel>();
                        viewModel.LoadItemsCommand.Execute(null);
                        return viewModel;
                    }
                },
                { MainView.Product, () => _viewModelService.Get<ProductViewModel>() },
                { MainView.Group, () => _viewModelService.Get<GroupViewModel>() },
            };
            var viewModel = mainViews[@event.View];
            MainViewModel = viewModel.Invoke();
        }

        [RelayCommand]
        private async void LoadViews()
        {
            MenuViewModel = _viewModelService.Get<MainMenuViewModel>();
            MainViewModel = _viewModelService.Get<PurchasesViewModel>();
        }
    }
}
