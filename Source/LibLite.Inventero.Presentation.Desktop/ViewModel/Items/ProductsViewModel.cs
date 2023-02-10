using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel.Items
{
    public partial class ProductsViewModel : ItemsViewModel<Product, IProductStore>
    {
        public ProductsViewModel(
            IProductStore store,
            IDialogService dialogService,
            IViewService viewService)
            : base(store, dialogService, viewService) { }

        protected override void AddItem()
        {
            _viewService.ShowProduct();
        }

        protected override void EditItem(Product item)
        {
            _viewService.ShowProduct(item);
        }

        protected override List<Column> CreateColumns()
        {
            return new List<Column>
            {
                new Column(Strings.ProductsGroupNameHeader, $"{nameof(Product.Group)}.{nameof(Product.Group.Name)}"),
                new Column(Strings.ProductsNameHeader, nameof(Product.Name)),
                new Column(Strings.ProductsPriceHeader, nameof(Product.Price)),
            };
        }
    }
}