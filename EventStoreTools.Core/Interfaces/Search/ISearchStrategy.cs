
namespace EventStoreTools.Core.Interfaces.Search
{
    public abstract class ISearchStrategy<T, TT>
    {
        public ISearchStrategy(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public abstract bool Compare(TT @event);
    }
}
