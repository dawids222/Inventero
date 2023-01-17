namespace LibLite.Inventero.Core.Contracts.Tools
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> callback);
        void Publish<TEvent>(TEvent @event);
    }
}
