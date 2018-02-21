using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.DTO.Entities.Search;
using System.Collections.Generic;

namespace EventStoreTools.Core.Interfaces.Search
{
    public interface ISearchService
    {
        IEnumerable<Event> StreamSearch(string stream, SearchParamsDTO searchParams);
        IEnumerable<Event> StreamSearchByEventNumber(string stream, long eventNumber, SearchParamsDTO searchParams);
        IEnumerable<Event> EventsSearch(SearchParamsDTO searchParams);
    }
}
