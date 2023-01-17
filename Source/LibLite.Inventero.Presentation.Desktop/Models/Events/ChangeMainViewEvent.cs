using LibLite.Inventero.Presentation.Desktop.Enums;

namespace LibLite.Inventero.Presentation.Desktop.Models.Events
{
    public class ChangeMainViewEvent
    {
        public MainView View { get; }

        public ChangeMainViewEvent(MainView view)
        {
            View = view;
        }
    }
}
