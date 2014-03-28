using System;
using Commands;
using Infrastructure;
using StructureMap;

namespace Bus
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory.Initialize(cfg => cfg.Scan(scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf(typeof(ICommandExecutor<>));
                    scanner.AddAllTypesOf(typeof(IHandleEvents<>));
                }));
            
            var bus = Configure.With()
                .StructureMapBuilder()
                .CreateDefaultBus();


            string accountName;
            decimal accountBalance;

            Console.WriteLine("Enter account name:");

            while ((accountName = Console.ReadLine()) != "exit")
            {
                Console.WriteLine("Enter account balance:");
                accountBalance = decimal.Parse(Console.ReadLine());

                var cmd = new OpenAccountCommand { AccountName = accountName, InitialBalance = accountBalance };
                bus.Send(cmd);

                Console.WriteLine("\nEnter Account Name:");
            }

            Console.ReadLine();
        }
        }
    }