﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class PurchaseViewModel : RelationshipItemViewModel<Purchase, IPurchaseStore, Product, IProductStore>
    {
        [ObservableProperty]
        private Product _product;
        [ObservableProperty]
        private double _unitPrice;
        [ObservableProperty]
        private int _amount;
        [ObservableProperty]
        private DateTime _date = DateTime.UtcNow;

        public PurchaseViewModel(
            IPurchaseStore store,
            IViewService viewService,
            IDialogService dialogService,
            IProductStore relationshipStore)
            : base(store, viewService, dialogService, relationshipStore) { }

        protected override IEnumerable<Input> CreateInputs()
        {
            return new Input[]
            {
                new SelectInput(
                    Strings.PurchaseProductLabel,
                    nameof(Product),
                    nameof(Search),
                    nameof(Relationships),
                    nameof(Core.Models.Domain.Product.Name),
                    nameof(SearchRelationshipsCommand),
                    nameof(UpdateUnitPriceCommand)),
                new NumberInput(Strings.PurchaseUnitPriceLabel, nameof(UnitPrice)),
                new NumberInput(Strings.PurchaseAmountLabel, nameof(Amount)),
                new DateInput(Strings.PurchaseDateLabel, nameof(Date)),
            };
        }

        protected override Purchase CreateItem()
        {
            return new Purchase(Id, Amount, UnitPrice, Date, Product);
        }

        protected override bool ValidateItem(Purchase item)
        {
            return
                item.Product is not null &&
                item.UnitPrice >= 0 &&
                item.Amount > 0;
        }

        public override void LoadItem(Purchase item)
        {
            Id = item.Id;
            Amount = item.Amount;
            UnitPrice = item.UnitPrice;
            Date = item.Date;
            Product = item.Product;
        }

        protected override void GoBack()
        {
            _viewService.ShowPurchases();
        }

        [RelayCommand]
        private void UpdateUnitPrice()
        {
            UnitPrice = Product?.Price ?? default;
        }
    }
}
