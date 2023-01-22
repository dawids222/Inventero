﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public abstract partial class PaginatedListViewModel<TItem, TStore> : ObservableObject
        where TItem : Identifiable
        where TStore : IStore<TItem>
    {
        private readonly TStore _store;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private int _pageNumber = 1;
        [ObservableProperty]
        private int _pageSize = 20;
        [ObservableProperty]
        private string _search = string.Empty;
        [ObservableProperty]
        private PaginatedList<TItem> _items;
        [ObservableProperty]
        private List<Column> _columns;

        protected PaginatedListViewModel(TStore store, IDialogService dialogService)
        {
            _store = store;
            _dialogService = dialogService;
            Initialize();
        }

        private void Initialize()
        {
            CreateDataGrid();
            LoadItems();
        }

        private void CreateDataGrid()
        {
            Columns = new List<Column>();
            CreateDataGridColumns(Columns);
        }

        protected abstract void CreateDataGridColumns(List<Column> columns);

        [RelayCommand]
        private async void LoadItems()
        {
            await _dialogService.ShowLoadingAsync();
            var request = CreateItemsRequest();
            Items = await _store.GetAsync(request);
            await _dialogService.HideLoadingAsync();
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
        private async void EditItem(TItem item)
        {
            return;
        }

        [RelayCommand]
        private async void DeleteItem(TItem item)
        {
            return;
        }

        [RelayCommand]
        protected abstract void AddItem();

        private PaginatedListRequest CreateItemsRequest()
        {
            return new PaginatedListRequest
            {
                PageIndex = PageNumber - 1,
                PageSize = PageSize,
                Search = Search,
            };
        }

        protected DataGridTextColumn CreateDataGridTextColumn(string header, string binding)
        {
            return new DataGridTextColumn
            {
                Header = header,
                Binding = new Binding(binding),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                FontSize = 16,
                IsReadOnly = true,
                CanUserSort = false,
            };
        }
    }
}
