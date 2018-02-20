using EventStore.ClientAPI;

namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public interface IEventStoreConnectionFactory
    {
        IEventStoreConnection Create(string connectionString);
    }
}
