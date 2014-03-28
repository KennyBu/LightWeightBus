namespace Infrastructure
{
    public interface IHandleEvents<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}