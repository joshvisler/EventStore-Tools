using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStoreTools.Core.Entities
{
    public class Connection
    {
        public Connection() { }
        public Connection(Guid connectionId, string name, string connectionString, bool isDefault, int roleId, string serviceAddress)
        {
            ConnectionId = connectionId;
            Name = name;
            ConnectionString = connectionString;
            IsDefault = isDefault;
            RoleId = roleId;
            ServiceAddress = serviceAddress;
        }

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
        [Column("serviceaddress")]
        public string ServiceAddress { get; private set; }


        public Role Role { get; private set; }
    }
}
