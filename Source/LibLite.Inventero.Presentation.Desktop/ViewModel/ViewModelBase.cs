using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public abstract partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private bool _isLoaded = false;

        protected virtual Task LoadAsync() { return Task.CompletedTask; }
        protected virtual Task UnloadAsync() { return Task.CompletedTask; }

        public async Task OnLoadedAsync()
        {
            if (IsLoaded) { return; }
            await LoadAsync();
            IsLoaded = true;
        }

        public async Task OnUnloadedAsync()
        {
            if (!IsLoaded) { return; }
            await UnloadAsync();
            IsLoaded = false;
        }
    }
}
