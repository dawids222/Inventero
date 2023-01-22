using LibLite.Inventero.Presentation.Desktop.Interfaces;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.Services
{
    public class DialogService : IDialogService
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        private ProgressDialogController _loading;

        public DialogService(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
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
    }
}
