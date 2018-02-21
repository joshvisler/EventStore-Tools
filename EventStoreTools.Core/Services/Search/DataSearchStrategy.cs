using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Interfaces.Search;

namespace EventStoreTools.Core.Services.Search
{
    public class DataSearchStrategy : ISearchStrategy<object, Event>
    {
        public DataSearchStrategy(string value) : base(value)
        {
        }

        public override bool Compare(Event @event)
        {
            if (@event.Data.Contains((string)Value) || @event.Data.ToLower().Contains(((string)Value).ToLower()))
                return true;
            return false;
        }
    }
}
