using EventStore.ClientAPI;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public class EventStoreConnectionFactory : IEventStoreConnectionFactory
    {
       // private readonly ILogger _logger;

        public EventStoreConnectionFactory(/*ILogger logger*/)
        {
           // _logger = logger;
        }

        public IEventStoreConnectionContext Create(string connectionString)
        {
            return new EventStoreConnectionContext(connectionString/*, _logger*/);
        }
    }
}
