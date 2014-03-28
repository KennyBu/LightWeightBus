namespace Infrastructure
{
    public interface ICommandExecutor<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}