using LibLite.Bus.Lite;
using LibLite.Inventero.Core.Contracts.Tools;

namespace LibLite.Inventero.Adapter.Tools
{
    public class BusLiteEventBus : IEventBus
    {
        private readonly EventBus _bus = new();

        public void Subscribe<TEvent>(Action<TEvent> callback)
        {
            _bus.Subscribe(this, callback);
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _bus.Notify(@event);
        }
    }
}
