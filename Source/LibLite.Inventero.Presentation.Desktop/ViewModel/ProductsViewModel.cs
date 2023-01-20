using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductStore _store;

        [ObservableProperty]
        private PaginatedList<Product> _products;

        [ObservableProperty]
        private int _pageNumber = 1;
        [ObservableProperty]
        private int _pageSize = 20;
        [ObservableProperty]
        private string _search = string.Empty;

        public ProductsViewModel(IProductStore store)
        {
            _store = store;

            Initialize();
        }

        private void Initialize()
        {
            LoadItems();
        }

        [RelayCommand]
        private void DecrementPage()
        {
            if (PageNumber == 1) { return; }
            PageNumber--;
        }

        [RelayCommand]
        private void IncrementPage()
        {
            PageNumber++;
        }

        [RelayCommand]
        private async void LoadItems()
        {
            var request = CreateItemsRequest();
            Products = await _store.GetAsync(request);
        }

        private PaginatedListRequest CreateItemsRequest()
        {
            return new PaginatedListRequest
            {
                PageIndex = PageNumber - 1,
                PageSize = PageSize,
                Search = Search,
            };
        }
    }
}
