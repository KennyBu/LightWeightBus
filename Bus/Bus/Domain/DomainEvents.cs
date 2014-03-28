using Infrastructure;
using StructureMap;

namespace Domain
{
    public static class DomainEvents
    {
        private static IEventBus eventBus;

        static DomainEvents()
        {
            eventBus = ObjectFactory.GetInstance<IEventBus>();
        }

        public static void Raise<TEvent>(TEvent @event) where TEvent : IEvent
        {
            eventBus.Publish(@event);
        }
    }
}