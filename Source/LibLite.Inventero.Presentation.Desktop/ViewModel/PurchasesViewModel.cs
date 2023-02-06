using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Contracts.Tools;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Enums;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Events;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class PurchasesViewModel : PaginatedListViewModel<Purchase, IPurchaseStore>
    {
        private readonly IEventBus _bus;

        public PurchasesViewModel(IPurchaseStore store, IDialogService dialogService, IEventBus bus)
            : base(store, dialogService)
        {
            _bus = bus;
        }

        protected override void AddItem()
        {
            var @event = new ChangeMainViewEvent(MainView.Purchase);
            _bus.Publish(@event);
        }

        protected override void CreateDataGridColumns(List<Column> columns)
        {
            columns.Add(new Column("Produkt", $"{nameof(Purchase.Product)}.{nameof(Purchase.Product.Name)}"));
            columns.Add(new Column("Liczba", nameof(Purchase.Amount)));
            columns.Add(new Column("Cena Jednostkowa", nameof(Purchase.UnitPrice)));
            columns.Add(new Column("Data Zakupu", nameof(Purchase.Date)));
        }
    }
}
