using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class PurchasesViewModel : ObservableObject
    {
        private readonly IPurchaseStore _store;

        [ObservableProperty]
        private PaginatedList<Purchase> _purchases;

        public PurchasesViewModel(IPurchaseStore store)
        {
            _store = store;

            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Purchases = await _store.GetAsync(new PaginatedListRequest());
        }
    }
}
