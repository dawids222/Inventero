using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Core.Models.Pagination;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using System.Collections.Generic;
using System.Windows.Input;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public abstract class Input
    {
        public string Label { get; set; }
        public string Binding { get; set; }

        protected Input(string label, string binding)
        {
            Label = label;
            Binding = binding;
        }
    }

    public class StringInput : Input
    {
        public StringInput(string label, string binding) : base(label, binding) { }
    }

    public class DoubleInput : Input
    {
        public DoubleInput(string label, string binding) : base(label, binding) { }
    }

    public class DatePickerInput : Input
    {
        public DatePickerInput(string label, string binding) : base(label, binding) { }
    }

    public class DropDownInput : Input
    {
        public string SearchBinding { get; set; }
        public string SelectedItemBinding { get; set; }
        public string ItemsBinding { get; set; }
        public string DisplayMember { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public DropDownInput(
            string label,
            string binding,
            string searchBinding,
            string selectedItemBinding,
            string itemsBinding,
            string displayMember,
            ICommand searchCommand,
            ICommand selectionChangedCommand) : base(label, binding)
        {
            SearchBinding = searchBinding;
            SelectedItemBinding = selectedItemBinding;
            ItemsBinding = itemsBinding;
            DisplayMember = displayMember;
            SearchCommand = searchCommand;
            SelectionChangedCommand = selectionChangedCommand;
        }
    }

    public abstract partial class ItemViewModel : ObservableObject
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

        protected long Id { get; set; }

        protected ItemViewModel(TStore store, IViewService viewService)
        {
            _store = store;
            _viewService = viewService;
        }

        [RelayCommand]
        protected abstract void Load();
        protected abstract TItem CreateItem();
        protected abstract bool ValidateItem(TItem item);

        [RelayCommand]
        private async void AddItem()
        {
            var item = CreateItem();
            if (!ValidateItem(item)) { return; } // TODO: Show error message?
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

    public abstract partial class RelationshipItemViewModel<TItem, TStore, TRelationshipItem, TRelationshipStore> : ItemViewModel<TItem, TStore>
        where TItem : Identifiable
        where TStore : IStore<TItem>
        where TRelationshipItem : Identifiable
        where TRelationshipStore : IStore<TRelationshipItem>
    {
        protected readonly TRelationshipStore _relationshipStore;

        private bool _selected = false;

        protected RelationshipItemViewModel(
            TStore store,
            IViewService viewService,
            TRelationshipStore relationshipStore)
            : base(store, viewService)
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
            if (_selected)
            {
                _selected = false;
                return;
            }
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
            IGroupStore relationshipStore)
            : base(store, viewService, relationshipStore) { }

        protected override IEnumerable<Input> CreateInputs()
        {
            return new Input[]
            {
                new StringInput("Nazwa", nameof(Name)),
                new DoubleInput("Cena", nameof(Price)),
                new DropDownInput(
                    "Kategoria",
                    nameof(Group),
                    nameof(GroupSearch),
                    nameof(Group),
                    nameof(Groups),
                    nameof(Core.Models.Domain.Group.Name),
                    SearchCommand,
                    SelectedCommand),
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
            Id = item.Id;
            Name = item.Name;
            Price = item.Price;
            Group = item.Group;
        }

        protected override void GoBack()
        {
            _viewService.ShowProductsView();
        }
    }
}
