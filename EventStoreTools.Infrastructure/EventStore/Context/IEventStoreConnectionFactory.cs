
namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public interface IEventStoreConnectionFactory
    {
        IEventStoreConnectionContext Create(string connectionString);
    }
}
