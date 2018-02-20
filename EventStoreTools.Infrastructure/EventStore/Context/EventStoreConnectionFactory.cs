using EventStore.ClientAPI;

namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public class EventStoreConnectionFactory : IEventStoreConnectionFactory
    {
        private readonly ILogger _logger;

        public EventStoreConnectionFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IEventStoreConnection Create(string connectionString)
        {
            var connectionContext = new EventStoreConnectionContext(connectionString, _logger);
            return connectionContext.Connection;
        }
    }
}
