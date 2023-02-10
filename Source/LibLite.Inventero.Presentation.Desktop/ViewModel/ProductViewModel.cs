using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public abstract partial class ItemViewModel : ViewModelBase
    {
        public IEnumerable<Input> Inputs { get; private set; }

        public ItemViewModel()
        {
            Inputs = CreateInputs();
        }

        protected abstract IEnumerable<Input> CreateInputs();
    }

    public abstract partial class ItemViewModel<TItem, TStore> : ItemViewModel
        where TItem : Identifiable
        where TStore : IStore<TItem>
    {
        protected readonly TStore _store;
        protected readonly IViewService _viewService;
        protected readonly IDialogService _dialogService;

        protected long Id { get; set; }

        protected ItemViewModel(
            TStore store,
            IViewService viewService,
            IDialogService dialogService)
        {
            _store = store;
            _viewService = viewService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        protected abstract void Load();
        protected abstract TItem CreateItem();
        protected abstract bool ValidateItem(TItem item);

        [RelayCommand]
        private async Task AddItem()
        {
            var item = CreateItem();
            if (!ValidateItem(item))
            {
                // TODO: Move to resources.
                // TODO: Provide more reasonable messages.
                var error = "Wpisane dane są niepoprawne.";
                await _dialogService.ShowErrorAsync(error);
                return;
            }
            await _store.StoreAsync(item);
            GoBack();
        }

        [RelayCommand]
        private async void Cancel()
        {
            GoBack();
        }

        protected abstract void GoBack();

        public abstract void LoadItem(TItem item);
    }

    public abstract partial class RelationshipItemViewModel<TItem, TStore, TRelationshipItem, TRelationshipStore>
        : ItemViewModel<TItem, TStore>
        where TItem : Identifiable
        where TStore : IStore<TItem>
        where TRelationshipItem : Identifiable
        where TRelationshipStore : IStore<TRelationshipItem>
    {
        protected readonly TRelationshipStore _relationshipStore;

        protected bool _selected = false;

        protected RelationshipItemViewModel(
            TStore store,
            IViewService viewService,
            IDialogService dialogService,
            TRelationshipStore relationshipStore)
            : base(store, viewService, dialogService)
        {
            _relationshipStore = relationshipStore;
        }

        protected abstract void SearchRelationship();

        protected override void Load()
        {
            Search();
        }

        [RelayCommand]
        protected async void Search()
        {
            SearchRelationship();
        }

        [RelayCommand]
        protected virtual async void Selected()
        {
            _selected = true;
            return;
        }
    }

    public partial class ProductViewModel : RelationshipItemViewModel<Product, IProductStore, Group, IGroupStore>
    {
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private double _price = 0;
        [ObservableProperty]
        private string _groupSearch = string.Empty;
        [ObservableProperty]
        private Group _group;
        [ObservableProperty]
        private PaginatedList<Group> _groups;

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
                new StringInput("Nazwa", nameof(Name)),
                new NumberInput("Cena", nameof(Price)),
                new SelectInput(
                    "Kategoria",
                    nameof(Group),
                    nameof(GroupSearch),
                    nameof(Groups),
                    nameof(Core.Models.Domain.Group.Name),
                    nameof(SearchCommand)),
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

        protected override async void SearchRelationship()
        {
            var request = new PaginatedListRequest()
            {
                PageSize = 10,
                Search = GroupSearch,
            };
            Groups = await _relationshipStore.GetAsync(request);
        }

        public override void LoadItem(Product item)
        {
            _selected = true;

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
