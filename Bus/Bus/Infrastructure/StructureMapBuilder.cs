using System.Collections.Generic;
using StructureMap;

namespace Infrastructure
{
    public class StructureMapBuilder : ICommandExecutorFactory, IEventHandlerFactory {
        
        private readonly IContainer container;

        public StructureMapBuilder(IContainer container) {
            this.container = container;
        }

        public ICommandExecutor<TCommand> GetExecutorFor<TCommand>() where TCommand : ICommand {
            return container.GetInstance<ICommandExecutor<TCommand>>();
        }

        public IEnumerable<IHandleEvents<TEvent>> GetHandlersFor<TEvent>() where TEvent : IEvent {
            return container.GetAllInstances<IHandleEvents<TEvent>>();
        } 
    }
}