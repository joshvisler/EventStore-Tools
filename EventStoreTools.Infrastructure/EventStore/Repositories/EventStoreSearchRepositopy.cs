using EventStoreTools.Core.Interfaces.Search;
using System.Collections.Generic;
using EventStore.ClientAPI;
using EventStoreTools.Infrastructure.EventStore.Context;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace EventStoreTools.Infrastructure.EventStore.Repositories
{
    public class EventStoreSearchRepositopy : IEventStoreSearchRepositopy
    {
        //private readonly ILogger _logget;
        private readonly IEventStoreConnectionFactory _eventStoreConnectionFactory;
        private const int _readEventsCount = 2500;

        public EventStoreSearchRepositopy(/*ILogger logger, */IEventStoreConnectionFactory eventStoreConnectionFactory)
        {
            //_logget = logger;
            _eventStoreConnectionFactory = eventStoreConnectionFactory;
        }

        public async Task<EventReadResult> SearchByEventNumberAsync(string stream, long eventNumber, string eventStoreConnectionString)
        {
            var eventStoreConnectionContext = _eventStoreConnectionFactory.Create(eventStoreConnectionString);
            eventStoreConnectionContext.Connect();

            return await eventStoreConnectionContext.Connection.ReadEventAsync(stream, eventNumber, false);
        }

        public async Task<IEnumerable<ResolvedEvent>> SearchInAllEventsAsync(string eventStoreConnectionString)
        {
            return await Task.Factory.StartNew<IEnumerable<ResolvedEvent>>( ()=>
            {
                var events = new List<ResolvedEvent>();
                var currentPosition = Position.Start;
                using (var eventStoreConnectionContext = _eventStoreConnectionFactory.Create(eventStoreConnectionString))
                {
                    eventStoreConnectionContext.Connect();

                    while (currentPosition != Position.End)
                    {
                        var readedEvents = eventStoreConnectionContext.Connection.ReadAllEventsForwardAsync(currentPosition, _readEventsCount, false).Result;
                        currentPosition = readedEvents.NextPosition;

                        if (readedEvents.Events == null || readedEvents.Events.LongLength == 0)
                        {
                            continue;
                        }

                        events.AddRange(readedEvents.Events);
                    }
                }

                return events;
            });
        }

        public async Task<IEnumerable<ResolvedEvent>> SearchInStreamAsync(string streamId, string eventStoreConnectionString)
        {
          
                return await Task.Factory.StartNew<IEnumerable<ResolvedEvent>>(() =>
                {
                    var isLastEvent = false;
                    var events = new List<ResolvedEvent>();
                    using (var eventStoreConnectionContext = _eventStoreConnectionFactory.Create(eventStoreConnectionString))
                    {
                        eventStoreConnectionContext.Connect();
                        while (!isLastEvent)
                        {
                            var readedEvents = eventStoreConnectionContext.Connection.ReadStreamEventsForwardAsync(streamId, 0, _readEventsCount, false).Result;
                            isLastEvent = readedEvents.IsEndOfStream;

                            if (readedEvents.Events == null || readedEvents.Events.LongLength == 0)
                            {
                                continue;
                            }

                            events.AddRange(readedEvents.Events);
                        }
                    }
                    return events;
                });

        }
    }
}
