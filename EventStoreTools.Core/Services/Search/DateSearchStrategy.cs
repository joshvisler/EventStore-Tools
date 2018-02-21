using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Interfaces.Search;
using System;

namespace EventStoreTools.Core.Services.Search
{
    public class DateSearchStrategy : ISearchStrategy<object, Event>
    {
        public DateSearchStrategy(DateTime value) : base(value)
        {
        }

        public override bool Compare(Event @event)
        {
            if (@event.DateCreated >= (DateTime)Value || @event.DateCreated <= (DateTime)Value)
                return true;

            return false;
        }
    }
}
