using StructureMap;

namespace Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static Configuration StructureMapBuilder(this Configuration configuration)
        {
            return configuration.StructureMapBuilder(ObjectFactory.Container);
        }

        public static Configuration StructureMapBuilder(this Configuration configuration, IContainer container)
        {
            var builder = new StructureMapBuilder(container);
            return configuration
                .CommandExecutorFactoryOf(builder)
                .EventHandlerFactoryOf(builder);
        }

        public static DefaultBus CreateDefaultBus(this Configuration configuration)
        {
            var bus = new DefaultBus(configuration);

            // configuration.Builder.Register<IEventBus>(bus);
            // configuration.Builder.Register<ICommandBus>(bus);

            ObjectFactory.Configure(cfg =>
            {
                cfg.For<IEventBus>().Use(bus);
                cfg.For<ICommandBus>().Use(bus);
            });

            return bus;
        }
    }
}