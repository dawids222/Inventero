using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Consts;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public abstract partial class ItemsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private int _pageNumber = 1;
        [ObservableProperty]
        private int _pageSize = 20;
        [ObservableProperty]
        private string _search = string.Empty;
        [ObservableProperty]
        private List<Column> _columns;

        [RelayCommand] protected abstract Task LoadItemsAsync();
        [RelayCommand] protected abstract void AddItem();
        protected abstract List<Column> CreateColumns();

        protected override Task LoadAsync()
        {
            Columns = CreateColumns();
            return LoadItemsAsync();
        }
    }

    public abstract partial class ItemsViewModel<TItem, TStore> : ItemsViewModel
        where TItem : Identifiable
        where TStore : IStore<TItem>
    {
        private readonly TStore _store;
        private readonly IDialogService _dialogService;
        protected readonly IViewService _viewService;

        [ObservableProperty]
        private PaginatedList<TItem> _items;

        protected ItemsViewModel(
            TStore store,
            IDialogService dialogService,
            IViewService viewService)
        {
            _store = store;
            _dialogService = dialogService;
            _viewService = viewService;
        }

        protected override async Task LoadItemsAsync()
        {
            await _dialogService.ShowLoadingAsync();
            var request = CreateGetItemsRequest();
            Items = await _store.GetAsync(request);
            await _dialogService.HideLoadingAsync();
        }

        private PaginatedListRequest CreateGetItemsRequest()
        {
            return new PaginatedListRequest
            {
                PageIndex = PageNumber - 1,
                PageSize = PageSize,
                Search = Search,
                Sorts = CreateSortIdDesc(),
            };
        }

        private static IEnumerable<Sort> CreateSortIdDesc() =>
            new Sort[] { new(nameof(Identifiable.Id), SortDirection.DESC) };

        [RelayCommand]
        private void DecrementPage()
        {
            if (Items?.HasPreviousPage != true) { return; }
            PageNumber--;
        }

        [RelayCommand]
        private void IncrementPage()
        {
            if (Items?.HasNextPage != true) { return; }
            PageNumber++;
        }

        [RelayCommand]
        protected abstract void EditItem(TItem item);

        [RelayCommand]
        private Task DeleteItem(TItem item)
        {
            var callback = async () =>
            {
                await _store.DeleteAsync(item.Id);
                await LoadItemsAsync();
            };
            return _dialogService.ShowInfoAsync(Strings.ItemsDeleteMessageContent, callback);
        }
    }
}
