using Infrastructure;

namespace Commands
{
    public class OpenAccountCommand : ICommand
    {
        public string AccountName { get; set; }
        public decimal InitialBalance { get; set; }
    }
}