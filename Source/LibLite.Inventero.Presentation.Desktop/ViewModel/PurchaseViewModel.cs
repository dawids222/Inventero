using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Enums;
using System;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class PurchaseViewModel : RelationshipItemViewModel<Purchase, IPurchaseStore, Product, IProductStore>
    {
        [ObservableProperty]
        private string _productSearch = string.Empty;
        [ObservableProperty]
        private Product _product;
        [ObservableProperty]
        private PaginatedList<Product> _products;
        [ObservableProperty]
        private double _unitPrice;
        [ObservableProperty]
        private int _amount;
        [ObservableProperty]
        private DateTime _date = DateTime.UtcNow;

        public PurchaseViewModel(IPurchaseStore store, IProductStore relationshipStore, IEventBus bus)
            : base(store, relationshipStore, bus) { }

        protected override MainView PreviousView => MainView.Purchases;

        protected override IEnumerable<Input> CreateInputs()
        {
            return new Input[]
            {
                new DropDownInput(
                    "Produkt",
                    nameof(Product),
                    nameof(ProductSearch),
                    nameof(Product),
                    nameof(Products),
                    nameof(Core.Models.Domain.Product.Name),
                    SearchCommand,
                    SelectedCommand),
                new DoubleInput("Cena jednostkowa", nameof(UnitPrice)),
                new StringInput("Liczba", nameof(Amount)),
                new DatePickerInput("Data", nameof(Date)),
            };
        }

        protected override Purchase CreateItem()
        {
            return new Purchase(Guid.Empty, Amount, UnitPrice, Date, Product);
        }

        protected override bool ValidateItem(Purchase item)
        {
            return
                item.Product is not null &&
                item.UnitPrice >= 0 &&
                item.Amount > 0;
        }

        protected override async void SearchRelationship()
        {
            var request = new PaginatedListRequest()
            {
                PageSize = 10,
                Search = ProductSearch,
            };
            Products = await _relationshipStore.GetAsync(request);
        }

        protected override void Selected()
        {
            base.Selected();
            UnitPrice = Product.Price;
        }
    }
}
