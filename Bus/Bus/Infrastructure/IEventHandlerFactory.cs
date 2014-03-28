using System.Collections.Generic;

namespace Infrastructure
{
    public interface IEventHandlerFactory
    {
         IEnumerable<IHandleEvents<TEvent>> GetHandlersFor<TEvent>() where TEvent : IEvent;
    }
}