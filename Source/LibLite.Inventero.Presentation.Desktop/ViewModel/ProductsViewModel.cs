using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductStore _store;

        [ObservableProperty]
        private PaginatedList<Product> _products;

        public ProductsViewModel(IProductStore store)
        {
            _store = store;

            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Products = await _store.GetAsync(new PaginatedListRequest());
        }
    }
}
