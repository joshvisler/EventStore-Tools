using System.Collections.Generic;
using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.DTO.Entities.Search;
using EventStoreTools.Core.Services.Search.Factories;
using EventStoreTools.Core.Interfaces.Search;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Exceptions;
using System;

namespace EventStoreTools.Core.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchStrategyFactory _searchStrategyFactory;
        private readonly IEventStoreSearchRepositopy _eventStoreSearchRepositopy;
        private readonly IConnectionRepository _connectionRepository;

        public SearchService(ISearchStrategyFactory searchStrategyFactory, IEventStoreSearchRepositopy eventStoreSearchRepositopy,
            IConnectionRepository connectionRepository)
        {
            _searchStrategyFactory = searchStrategyFactory;
            _eventStoreSearchRepositopy = eventStoreSearchRepositopy;
            _connectionRepository = connectionRepository;
        }

        public IEnumerable<Event> EventsSearch( SearchParamsDTO searchParams)
        {
            var domainEvents = new List<Event>();
            var searchStrategies = _searchStrategyFactory.Create(searchParams);
            var eventStoreConnectionString = GetConnectionString(searchParams.ConnectionId);

            var eventStoreEvent = _eventStoreSearchRepositopy.SearchInAllEventsAsync(eventStoreConnectionString).Result;

            foreach (var @event in eventStoreEvent)
            {
                var eventData = System.Text.Encoding.UTF8.GetString(@event.Event.Data);
                var domainEvent = new Event(@event.Event.EventStreamId, @event.Event.EventNumber, @event.Event.Created, eventData, @event.Event.EventType);
                domainEvents.Add(domainEvent);
            }

            return domainEvents;
        }

        public IEnumerable<Event> StreamSearch(string stream, SearchParamsDTO searchParams)
        {
            var domainEvents = new List<Event>();
            var searchStrategies = _searchStrategyFactory.Create(searchParams);
            var eventStoreConnectionString = GetConnectionString(searchParams.ConnectionId);

            var eventStoreEvent = _eventStoreSearchRepositopy.SearchInStreamAsync(stream, eventStoreConnectionString).Result;

            foreach (var @event in eventStoreEvent)
            {
                var eventData = System.Text.Encoding.UTF8.GetString(@event.Event.Data);
                var domainEvent = new Event(@event.Event.EventStreamId, @event.Event.EventNumber, @event.Event.Created, eventData, @event.Event.EventType);
                domainEvents.Add(domainEvent);
            }

            return domainEvents;
        }

        public IEnumerable<Event> StreamSearchByEventNumber(string stream, long eventNumber, SearchParamsDTO searchParams)
        {
            var eventStoreConnectionString = GetConnectionString(searchParams.ConnectionId);
            var searchTask = _eventStoreSearchRepositopy.SearchByEventNumberAsync(stream, eventNumber, eventStoreConnectionString);
            searchTask.Wait();
            var eventStoreEvent = searchTask.Result;

            var domainEvents = new List<Event>();

            if (eventStoreEvent != null && eventStoreEvent.Event.HasValue)
            {
                var eventData = System.Text.Encoding.UTF8.GetString(eventStoreEvent.Event.Value.Event.Data);
                var @event = new Event(eventStoreEvent.Stream, eventStoreEvent.EventNumber, eventStoreEvent.Event.Value.Event.Created, eventData, eventStoreEvent.Event.Value.Event.EventType);
                domainEvents.Add(@event);
            }

            return domainEvents;
        }

        private string GetConnectionString(Guid connectionId)
        {
            var eventStoreConnection = _connectionRepository.GetById(connectionId);

            if (eventStoreConnection == null)
                throw new EventStoreConnectionNotFoundException();

            return eventStoreConnection.ConnectionString;
        }
    }
}
