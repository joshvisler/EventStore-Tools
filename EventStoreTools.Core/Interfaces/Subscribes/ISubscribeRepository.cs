using EventStoreTools.Core.Entities;

namespace EventStoreTools.Core.Interfaces.Subscribes
{
    public interface ISubscribeRepository : IRepository<Subscribe>, IRepositoryAsync<Subscribe>
    {
    }
}
