using System;

namespace EventStoreTools.DTO.Entities.Search
{
    public class SearchParamsDTO
    {
        public string Data { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Guid ConnectionId { get; set; }
    }
}
