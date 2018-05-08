using System;

namespace EventStoreTools.Core.Entities
{
    public class FromToParam
    {
        public FromToParam(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

    }
}
