﻿using System;
using System.Collections.Generic;
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

            if (searchParams.From != null)
            {
                searchStrategies.Add(new DateSearchStrategy(searchParams.From));
            }
            else if (searchParams.To != null)
            {
                searchStrategies.Add(new DateSearchStrategy(searchParams.To));

            }
            else if(searchParams.Data != null && !string.IsNullOrEmpty(searchParams.Data))
            {
                searchStrategies.Add(new DataSearchStrategy(searchParams.Data));
            }

            return searchStrategies;
        }
    }
}
