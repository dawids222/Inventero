using System.Threading.Tasks;

namespace LibLite.Inventero.Presentation.Desktop.Interfaces
{
    public interface IDialogService
    {
        Task ShowLoadingAsync();
        Task HideLoadingAsync();
    }
}
