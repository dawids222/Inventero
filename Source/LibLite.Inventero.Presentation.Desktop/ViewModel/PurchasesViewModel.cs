using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class PurchasesViewModel : ItemsViewModel<Purchase, IPurchaseStore>
    {
        public PurchasesViewModel(
            IPurchaseStore store,
            IDialogService dialogService,
            IViewService viewService)
            : base(store, dialogService, viewService) { }

        protected override void AddItem()
        {
            _viewService.ShowPurchase();
        }

        protected override void EditItem(Purchase item)
        {
            _viewService.ShowPurchase(item);
        }

        protected override List<Column> CreateColumns()
        {
            return new List<Column>
            {
                new Column(Strings.PurchasesProductNameHeader, $"{nameof(Purchase.Product)}.{nameof(Purchase.Product.Name)}"),
                new Column(Strings.PurchasesAmountHeader, nameof(Purchase.Amount)),
                new Column(Strings.PurchasesUnitPriceHeader, nameof(Purchase.UnitPrice)),
                new Column(Strings.PurchasesDateHeader, nameof(Purchase.Date), "d"),
            };
        }
    }
}
