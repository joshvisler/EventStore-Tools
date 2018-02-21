using EventStore.ClientAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces.Search
{
    public interface IEventStoreSearchRepositopy
    {
        Task<EventReadResult> SearchByEventNumberAsync(string stream, long eventNumber, string eventStoreConnectionString);
        Task<IEnumerable<ResolvedEvent>> SearchInStreamAsync(string streamId, string eventStoreConnectionString);
        Task<IEnumerable<ResolvedEvent>> SearchInAllEventsAsync(string eventStoreConnectionString);
    }
}
