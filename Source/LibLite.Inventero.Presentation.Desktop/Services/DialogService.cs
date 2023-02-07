using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.Services
{
    public class DialogService : IDialogService
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IDialogCoordinator _dialogCoordinator;

        public DialogService(
            MainWindowViewModel mainWindowViewModel,
            IDialogCoordinator dialogCoordinator)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _dialogCoordinator = dialogCoordinator;
        }

        public Task ShowLoadingAsync()
        {
            _mainWindowViewModel.IsLoading = true;
            return Task.CompletedTask;
        }

        public Task HideLoadingAsync()
        {
            _mainWindowViewModel.IsLoading = false;
            return Task.CompletedTask;
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
