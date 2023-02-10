using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System.Collections.Generic;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public partial class ProductViewModel : RelationshipItemViewModel<Product, IProductStore, Group, IGroupStore>
    {
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private double _price = 0;
        [ObservableProperty]
        private Group _group;

        public ProductViewModel(
            IProductStore store,
            IViewService viewService,
            IDialogService dialogService,
            IGroupStore relationshipStore)
            : base(store, viewService, dialogService, relationshipStore) { }

        protected override IEnumerable<Input> CreateInputs()
        {
            return new Input[]
            {
                new StringInput(Strings.ProductNameLabel, nameof(Name)),
                new NumberInput(Strings.ProductPriceLabel, nameof(Price)),
                new SelectInput(
                    Strings.ProductGroupLabel,
                    nameof(Group),
                    nameof(Search),
                    nameof(Relationships),
                    nameof(Core.Models.Domain.Group.Name),
                    nameof(SearchRelationshipsCommand)),
            };
        }

        protected override Product CreateItem()
        {
            return new Product(Id, Name, Price, Group);
        }

        protected override bool ValidateItem(Product item)
        {
            return
                !string.IsNullOrWhiteSpace(item.Name) &&
                item.Price >= 0 &&
                item.Group is not null;
        }

        public override void LoadItem(Product item)
        {
            Id = item.Id;
            Name = item.Name;
            Price = item.Price;
            Group = item.Group;
        }

        protected override void GoBack()
        {
            _viewService.ShowProducts();
        }
    }
}
