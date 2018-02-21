using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Interfaces.Search;
using EventStoreTools.DTO.Entities.Search;
using System.Collections.Generic;

namespace EventStoreTools.Core.Services.Search.Factories
{
    public interface ISearchStrategyFactory
    {
        IEnumerable<ISearchStrategy<object, Event>> Create(SearchParamsDTO searchParams);
    }
}
