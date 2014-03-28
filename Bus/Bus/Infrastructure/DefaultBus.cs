using System;
using System.Collections.Generic;
using System.Transactions;

namespace Infrastructure
{
    public class DefaultBus : ICommandBus, IEventBus
    {
        private readonly Configuration configuration;

        public DefaultBus(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            using (var txn = new TransactionScope())
            {
                //InvokeExecutor<TCommand>(GetCommandExecutor<TCommand>(), command);
                GetCommandExecutor<TCommand>().Execute(command);
                txn.Complete();
            }
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            foreach (var handler in GetEventHandlers<TEvent>())
            {
                InvokeHandler<TEvent>(handler, @event);
            }
        }

        private ICommandExecutor<TCommand> GetCommandExecutor<TCommand>() where TCommand : ICommand
        {
            var executor = configuration.CommandExecutorFactory.GetExecutorFor<TCommand>();
            if (executor == null)
            {
                throw new InvalidOperationException("No command executor registered for command type " + typeof(TCommand).FullName);
            }

            return executor;
        }

        private IEnumerable<IHandleEvents<TEvent>> GetEventHandlers<TEvent>() where TEvent : IEvent
        {
            return configuration.EventHandlerFactory.GetHandlersFor<TEvent>();
        }

        private static void InvokeExecutor<TCommand>(ICommandExecutor<TCommand> executor, TCommand command) where TCommand : ICommand
        {
            if (Transaction.Current != null)
            {
                Transaction.Current.EnlistVolatile(new CommandExecutorInvoker<TCommand>(executor, command), EnlistmentOptions.None);
            }
        }

        public static void InvokeHandler<TEvent>(IHandleEvents<TEvent> handler, TEvent @event) where TEvent : IEvent
        {
            if (Transaction.Current != null)
            {
                Transaction.Current.EnlistVolatile(new EventHandlerInvoker<TEvent>(handler, @event), EnlistmentOptions.None);
            }
        }

        private class CommandExecutorInvoker<TCommand> : IEnlistmentNotification where TCommand : ICommand
        {
            private readonly ICommandExecutor<TCommand> executor;
            private readonly TCommand command;

            public CommandExecutorInvoker(ICommandExecutor<TCommand> executor, TCommand command)
            {
                this.executor = executor;
                this.command = command;
            }

            public void Prepare(PreparingEnlistment preparingEnlistment)
            {
                preparingEnlistment.Prepared();
            }

            public void Commit(Enlistment enlistment)
            {
                executor.Execute(command);
                enlistment.Done();
            }

            public void Rollback(Enlistment enlistment)
            {
                enlistment.Done();
            }

            public void InDoubt(Enlistment enlistment)
            {
                enlistment.Done();
            }
        }

        private class EventHandlerInvoker<TEvent> : IEnlistmentNotification where TEvent : IEvent
        {
            private readonly IHandleEvents<TEvent> handler;
            private readonly TEvent @event;

            public EventHandlerInvoker(IHandleEvents<TEvent> handler, TEvent @event)
            {
                this.handler = handler;
                this.@event = @event;
            }

            public void Prepare(PreparingEnlistment preparingEnlistment)
            {
                preparingEnlistment.Prepared();
            }

            public void Commit(Enlistment enlistment)
            {
                handler.Handle(@event);
                enlistment.Done();
            }

            public void Rollback(Enlistment enlistment)
            {
                enlistment.Done();
            }

            public void InDoubt(Enlistment enlistment)
            {
                enlistment.Done();
            }
        }
    }
}