using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel.Item
{
    public abstract partial class RelationshipItemViewModel<TItem, TStore, TRelationshipItem, TRelationshipStore>
        : ItemViewModel<TItem, TStore>
        where TItem : Identifiable
        where TStore : IStore<TItem>
        where TRelationshipItem : Identifiable
        where TRelationshipStore : IStore<TRelationshipItem>
    {
        protected readonly TRelationshipStore _relationshipStore;

        [ObservableProperty]
        private string _search = string.Empty;
        [ObservableProperty]
        private PaginatedList<TRelationshipItem> _relationships;

        protected RelationshipItemViewModel(
            TStore store,
            IViewService viewService,
            IDialogService dialogService,
            TRelationshipStore relationshipStore)
            : base(store, viewService, dialogService)
        {
            _relationshipStore = relationshipStore;
        }

        protected override Task LoadAsync()
        {
            return SearchRelationshipsAsync();
        }

        [RelayCommand]
        private async Task SearchRelationshipsAsync()
        {
            var request = new PaginatedListRequest()
            {
                PageSize = 10,
                Search = Search,
            };
            Relationships = await _relationshipStore.GetAsync(request);
        }
    }
}
