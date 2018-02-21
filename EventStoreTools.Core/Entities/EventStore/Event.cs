using System;

namespace EventStoreTools.Core.Entities.EventStore
{
    public class Event
    {
        public string StreamId { get; private set; }
        public long EventNumber { get; private set; }
        public DateTime DateCreated { get; private set; }
        public string Data { get; private set; }
        public string Type { get; private set; }

        public Event(string streamid, long eventNumber, DateTime dateCreated, string data, string type)
        {
            StreamId = streamid;
            EventNumber = eventNumber;
            DateCreated = dateCreated;
            Data = data;
            Type = type;
        }
    }
}
