using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Models.Events;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly IProductStore _productStore;
        private readonly IGroupStore _groupStore;
        private readonly IEventBus _bus;

        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private double _price = 0;
        [ObservableProperty]
        private string _groupSearch = string.Empty;
        [ObservableProperty]
        private Group _group;
        [ObservableProperty]
        private PaginatedList<Group> _groups;

        private bool _selected = false;

        public ProductViewModel(
            IProductStore productStore,
            IGroupStore groupStore,
            IEventBus bus)
        {
            _productStore = productStore;
            _groupStore = groupStore;
            _bus = bus;
        }

        [RelayCommand]
        private void Load()
        {
            SearchGroups();
        }

        [RelayCommand]
        private async void SearchGroups()
        {
            if (_selected)
            {
                _selected = false;
                return;
            }

            var request = new PaginatedListRequest()
            {
                PageSize = 10,
                Search = GroupSearch,
            };
            Groups = await _groupStore.GetAsync(request);
        }

        [RelayCommand]
        private async void AddProduct()
        {
            if (!ValidateProduct()) { return; } // TODO: Show error message?
            var product = new Product(Name, Price, Group);
            await _productStore.StoreAsync(product);
            GoBack();
        }

        private bool ValidateProduct()
        {
            return
                !string.IsNullOrEmpty(Name) &&
                Price >= 0 &&
                Group is not null;
        }

        [RelayCommand]
        private async void Cancel()
        {
            GoBack();
        }

        private void GoBack()
        {
            var @event = new ChangeMainViewEvent(MainView.Products);
            _bus.Publish(@event);
        }

        [RelayCommand]
        private async void Selected()
        {
            _selected = true;
            return;
        }
    }
}
