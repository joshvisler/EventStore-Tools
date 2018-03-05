
namespace EventStoreTools.Core.Entities.QueryFilters
{
    public class LimitOfsetParameter
    {
        public LimitOfsetParameter(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;
        }

        private int Limit { get; }
        private int Offset { get; }
    }
}
