using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStoreTools.Core.Entities
{
    public class Subscribe
    {
        public Subscribe()
        {
        }

        public Subscribe(Guid subscribeId, Guid clientId, Guid connectionId, long lastStreamEvent, DateTime date, bool isActive)
        {
            SubscribeId = subscribeId;
            ClientId = clientId;
            ConnectionId = connectionId;
            LastStreamEvent = lastStreamEvent;
            Date = date;
            IsActive = isActive;
        }

        [Key]
        [Column("subscribeid")]
        public Guid SubscribeId { get; private set; }
        [ForeignKey("Client")]
        [Column("clientid")]
        public Guid ClientId { get; private set; }
        [ForeignKey("Connection")]
        [Column("connectionid")]
        public Guid ConnectionId { get; private set; }
        [Column("laststreamevent")]
        public long LastStreamEvent { get; private set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }

        public Client Client { get; private set; }
        public Connection Connection { get; private set; }
    }
}
