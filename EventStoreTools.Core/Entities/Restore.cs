using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStoreTools.Core.Entities
{
    public class Restore
    {
        public Restore()
        {
        }

        public Restore(Guid restoreId, Guid clientId, Guid connectionId, string progress, DateTime date)
        {
            RestoreId = restoreId;
            ClientId = clientId;
            ConnectionId = connectionId;
            Progress = progress;
            Date = date;
        }

        [Key]
        [Column("restoreid")]
        public Guid RestoreId { get; private set; }
        [ForeignKey("Client")]
        [Column("clientid")]
        public Guid ClientId { get; private set; }
        [ForeignKey("Connection")]
        [Column("connectionid")]
        public Guid ConnectionId { get; private set; }
        [Column("progress")]
        public string Progress { get; private set; }
        [Column("date")]
        public DateTime Date { get; set; }

        public Client Client { get; private set; }
        public Connection Connection { get; private set; }
    }
}
