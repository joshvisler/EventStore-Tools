using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Interfaces.Search;
using System;

namespace EventStoreTools.Core.Services.Search
{
    public class DateSearchStrategy : ISearchStrategy<object, Event>
    {
        public DateSearchStrategy(FromToParam value) : base(value)
        {
        }

        public override bool Compare(Event @event)
        {
            if (@event.DateCreated >= ((FromToParam)Value).From && @event.DateCreated <= ((FromToParam)Value).To)
                return true;

            return false;
        }
    }
}
