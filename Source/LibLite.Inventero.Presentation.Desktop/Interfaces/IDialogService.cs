using System;
using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.Interfaces
{
    public interface IDialogService
    {
        Task ShowLoadingAsync();
        Task HideLoadingAsync();
        Task ShowErrorAsync(string mssage);
        Task ShowInfoAsync(string message, Func<Task> onAffirmative);
    }
}
