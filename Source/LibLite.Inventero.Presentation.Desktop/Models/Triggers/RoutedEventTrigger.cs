using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace LibLite.Inventero.Presentation.Desktop.Models.Triggers
{
    public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
    {
        RoutedEvent _routedEvent;

        public RoutedEvent RoutedEvent
        {
            get { return _routedEvent; }
            set { _routedEvent = value; }
        }

        public RoutedEventTrigger() { }

        protected override void OnAttached()
        {
            FrameworkElement associatedElement = AssociatedObject as FrameworkElement;

            if (AssociatedObject is Behavior behavior)
            {
                associatedElement = AssociatedObject as FrameworkElement;
            }
            if (associatedElement == null)
            {
                throw new ArgumentException("Routed Event trigger can only be associated to framework elements");
            }
            if (RoutedEvent != null)
            {
                associatedElement.AddHandler(RoutedEvent, new RoutedEventHandler(OnRoutedEvent));
            }
        }
        void OnRoutedEvent(object sender, RoutedEventArgs args) => OnEvent(args);
        protected override string GetEventName() => RoutedEvent.Name;
    }
}
