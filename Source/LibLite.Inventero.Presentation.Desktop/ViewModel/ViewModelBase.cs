﻿using CommunityToolkit.Mvvm.ComponentModel;
using LibLite.Inventero.Presentation.Desktop.Resources;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.ViewModel
{
    public abstract partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private bool _isLoaded = false;

        public Strings Strings { get; } = new();

        protected virtual Task LoadAsync() { return Task.CompletedTask; }
        protected virtual Task UnloadAsync() { return Task.CompletedTask; }

        public async void OnLoaded()
        {
            if (IsLoaded) { return; }
            await LoadAsync();
            IsLoaded = true;
        }

        public async void OnUnloaded()
        {
            if (!IsLoaded) { return; }
            await UnloadAsync();
            IsLoaded = false;
        }
    }
}
