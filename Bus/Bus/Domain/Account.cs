using Events;

namespace Domain
{
    public class Account
    {
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string name, decimal initialBalance)
        {
            var @event =
                new AccountOpenedEvent
                {
                    AccountName = name,
                    InitialBalance = initialBalance
                };

            OnAccountOpened(@event);
            DomainEvents.Raise(@event);
        }

        protected void SetBalance(decimal balance)
        {
            Balance = balance;
            // Raise event? Set health
        }

        // events
        public void OnAccountOpened(AccountOpenedEvent @event)
        {
            Name = @event.AccountName;
            SetBalance(@event.InitialBalance);
        }
    }
}