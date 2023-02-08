using LibLite.Inventero.Presentation.Desktop.ViewModel;

namespace LibLite.Inventero.Presentation.Desktop.Interfaces
{
    public interface IViewModelService
    {
        T Get<T>() where T : ViewModelBase;
    }
}
