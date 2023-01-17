using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Models.Events;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IEventBus _eventBus;

        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly PurchasesViewModel _purchasesViewModel;
        private readonly ProductsViewModel _productsViewModel;
        private readonly GroupsViewModel _groupsViewModel;

        [ObservableProperty]
        private ObservableObject _menuViewModel;
        [ObservableProperty]
        private ObservableObject _mainViewModel;

        public MainWindowViewModel(
            IEventBus eventBus,
            MainMenuViewModel mainMenuViewModel,
            PurchasesViewModel purchasesViewModel,
            ProductsViewModel productsViewModel,
            GroupsViewModel groupsViewModel)
        {
            _eventBus = eventBus;

            _mainMenuViewModel = mainMenuViewModel;
            _purchasesViewModel = purchasesViewModel;
            _productsViewModel = productsViewModel;
            _groupsViewModel = groupsViewModel;

            MenuViewModel = _mainMenuViewModel;
            MainViewModel = _purchasesViewModel;

            _eventBus.Subscribe<ChangeMainViewEvent>(ChangeMainView);
        }

        private void ChangeMainView(ChangeMainViewEvent @event)
        {
            Dictionary<MainView, ObservableObject> mainViews = new()
            {
                { MainView.Purchases, _purchasesViewModel},
                { MainView.Products, _productsViewModel},
                { MainView.Groups, _groupsViewModel},
            };
            var viewModel = mainViews[@event.View];
            MainViewModel = viewModel;
        }
    }
}
