using System;
using System.Collections.Generic;
using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Interfaces.Search;
using EventStoreTools.DTO.Entities.Search;

namespace EventStoreTools.Core.Services.Search.Factories
{
    public class SearchStrategyFactory : ISearchStrategyFactory
    {
        public IEnumerable<ISearchStrategy<object, Event>> Create(SearchParamsDTO searchParams)
        {
            var searchStrategies = new List<ISearchStrategy<object, Event>>();

            if (searchParams.From != null && searchParams.From.HasValue && searchParams.To != null && searchParams.To.HasValue)
            {
                var param = new FromToParam(searchParams.From.Value, searchParams.To.Value);
                searchStrategies.Add(new DateSearchStrategy(param));
            }
            else if (searchParams.From != null && searchParams.From.HasValue)
            {
                var param = new FromToParam(searchParams.From.Value, DateTime.MaxValue);
                searchStrategies.Add(new DateSearchStrategy(param));

            }
            else if (searchParams.To != null && searchParams.To.HasValue)
            {
                var param = new FromToParam(DateTime.MinValue, searchParams.To.Value);
                searchStrategies.Add(new DateSearchStrategy(param));

            }
            else if(searchParams.Data != null && !string.IsNullOrEmpty(searchParams.Data))
            {
                searchStrategies.Add(new DataSearchStrategy(searchParams.Data));
            }

            return searchStrategies;
        }
    }
}
