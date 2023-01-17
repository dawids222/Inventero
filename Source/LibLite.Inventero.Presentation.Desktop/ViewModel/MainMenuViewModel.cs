using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Models.Events;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class MainMenuViewModel : ObservableObject
    {
        private readonly IEventBus _eventBus;

        public MainMenuViewModel(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [RelayCommand]
        private void ShowPurchasesView() => ChangeMainView(MainView.Purchases);
        [RelayCommand]
        private void ShowProductsView() => ChangeMainView(MainView.Products);
        [RelayCommand]
        private void ShowGroupsView() => ChangeMainView(MainView.Groups);
        [RelayCommand]
        private void ShowInventoryView() { }

        private void ChangeMainView(MainView view)
        {
            var @event = new ChangeMainViewEvent(view);
            _eventBus.Publish(@event);
        }
    }
}
