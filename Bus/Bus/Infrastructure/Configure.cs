namespace Infrastructure
{
    public static class Configure
    {
        private static Configuration currentConfiguration;

        internal static Configuration CurrentConfiguration
        {
            get { return currentConfiguration ?? (currentConfiguration = new Configuration()); }
        }

        public static Configuration With()
        {
            return CurrentConfiguration;
        }
    }

    public class Configuration
    {
        internal ICommandExecutorFactory CommandExecutorFactory { get; private set; }
        internal IEventHandlerFactory EventHandlerFactory { get; private set; }

        public Configuration CommandExecutorFactoryOf(ICommandExecutorFactory commandExecutorFactory)
        {
            CommandExecutorFactory = commandExecutorFactory;
            return this;
        }

        public Configuration EventHandlerFactoryOf(IEventHandlerFactory eventHandlerFactory)
        {
            EventHandlerFactory = eventHandlerFactory;
            return this;
        }
    }
}