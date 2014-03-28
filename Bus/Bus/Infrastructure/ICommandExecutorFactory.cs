namespace Infrastructure
{
    public interface ICommandExecutorFactory
    {
        ICommandExecutor<TCommand> GetExecutorFor<TCommand>() where TCommand : ICommand;
    }
}