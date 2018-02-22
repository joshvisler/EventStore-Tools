using System;

namespace EventStoreTools.DTO.Entities.Subscribes
{
    public class CreateSunscribeDTO
    {
        public long StartEventNumber { get; set; }
        public Guid ConnectionId { get; set; }
    }
}
