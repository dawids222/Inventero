using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.Services
{
    public class DialogService : IDialogService
    {
        private readonly TimeSpan LOADING_DELAY = TimeSpan.FromMilliseconds(250);

        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IDialogCoordinator _dialogCoordinator;

        public DialogService(
            MainWindowViewModel mainWindowViewModel,
            IDialogCoordinator dialogCoordinator)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _dialogCoordinator = dialogCoordinator;
        }

        public async Task ShowLoadingAsync()
        {
            _mainWindowViewModel.IsLoading = true;
            await Task.Delay(LOADING_DELAY);
        }

        public Task HideLoadingAsync()
        {
            _mainWindowViewModel.IsLoading = false;
            return Task.CompletedTask;
        }

        public async Task ShowInfoAsync(string message, Func<Task> onAffirmative)
        {
            var result = await _dialogCoordinator.ShowMessageAsync(_mainWindowViewModel, "Info", message, MessageDialogStyle.AffirmativeAndNegative);
            if (result != MessageDialogResult.Affirmative) { return; }
            await onAffirmative.Invoke();
        }

        public Task ShowErrorAsync(string message)
        {
            return _dialogCoordinator.ShowMessageAsync(_mainWindowViewModel, "Error", message, MessageDialogStyle.Affirmative, new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Inverted,
            });
        }
    }
}
