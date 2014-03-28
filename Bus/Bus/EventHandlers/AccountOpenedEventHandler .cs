using System;
using Events;
using Infrastructure;

namespace EventHandlers
{
    public class AccountOpenedEventHandler : IHandleEvents<AccountOpenedEvent>
    {
        public void Handle(AccountOpenedEvent @event)
        {
            Console.WriteLine("An account for {0} was opened with a balance of {1}",
                @event.AccountName,
                @event.InitialBalance);
        }
    }
}