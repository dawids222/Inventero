﻿using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class ProductsViewModel : PaginatedListViewModel<Product, IProductStore>
    {
        public ProductsViewModel(IProductStore store, IDialogService dialogService)
            : base(store, dialogService) { }

        protected override void AddItem()
        {
            return;
        }

        protected override void CreateDataGridColumns(List<Column> columns)
        {
            // TODO: Use string resources
            columns.Add(new Column("Kategoria", $"{nameof(Product.Group)}.{nameof(Product.Group.Name)}"));
            columns.Add(new Column("Nazwa", nameof(Product.Name)));
            columns.Add(new Column("Cena", nameof(Product.Price)));
        }
    }
}