using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStoreTools.Core.Entities
{
    public class Connection
    {
        [Key]
        [Column("connectionid")]
        public Guid ConnectionId { get; private set; }
        [Column("name")]
        public string Name { get; private set; }
        [Column("connectionstring")]
        public string ConnectionString { get; private set; }
        [Column("isdefault")]
        public bool IsDefault { get; set; }
        [ForeignKey("Role")]
        [Column("roleid")]
        public int RoleId { get; private set; }

        public Role Role { get; private set; }
    }
}
