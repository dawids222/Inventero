using CommunityToolkit.Mvvm.Input;
using LibLite.Inventero.Core.Contracts.Stores;
using LibLite.Inventero.Core.Models.Domain;
using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs;
using LibLite.Inventero.Presentation.Desktop.Resources;
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

        protected abstract TItem CreateItem();
        protected abstract bool ValidateItem(TItem item);
        protected abstract void GoBack();
        public abstract void LoadItem(TItem item);

        [RelayCommand]
        private async Task AddItem()
        {
            var item = CreateItem();
            if (!ValidateItem(item))
            {
                // TODO: Provide more reasonable messages.
                var error = Strings.ItemValidationErrorMessageContent;
                await _dialogService.ShowErrorAsync(error);
                return;
            }
            await _store.StoreAsync(item);
            GoBack();
        }

        [RelayCommand]
        private void Cancel()
        {
            GoBack();
        }
    }
}
