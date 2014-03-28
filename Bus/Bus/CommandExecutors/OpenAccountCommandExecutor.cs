using System;
using Commands;
using Domain;
using Infrastructure;

namespace CommandExecutors
{
    public class OpenAccountCommandExecutor : ICommandExecutor<OpenAccountCommand>
    {
        public void Execute(OpenAccountCommand command)
        {
            if (command.InitialBalance <= 0)
            {
                throw new InvalidOperationException("Cannot open an account with a negative balance");
            }

            var account = new Account(command.AccountName, command.InitialBalance);

            // save account
            Console.WriteLine("Account saved");
        }
    }
}