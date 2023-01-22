using CommunityToolkit.Mvvm.ComponentModel;

namespace LibLite.Inventero.Presentation.Desktop.Interfaces
{
    public interface IViewModelService
    {
        T Get<T>() where T : ObservableObject; // TODO: Change for dedicated ViewModel base class!!
    }
}
