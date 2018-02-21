using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Interfaces.Search;
using EventStoreTools.DTO.Entities.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{stream}")]
        public IEnumerable<Event> StreamSearch(string stream, SearchParamsDTO searchParams)
        {
            return _searchService.StreamSearch(stream, searchParams);
        }

        [HttpGet("{stream}/{eventNumber}")]
        public IEnumerable<Event> StreamSearch(string stream, long eventNumber, SearchParamsDTO searchParams)
        {
            return _searchService.StreamSearchByEventNumber(stream, eventNumber, searchParams);
        }

        [HttpGet("allevents")]
        public IEnumerable<Event> AllEventsSearch(SearchParamsDTO searchParams)
        {
            return _searchService.EventsSearch(searchParams);
        }
    }
}
