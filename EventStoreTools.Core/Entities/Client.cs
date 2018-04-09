using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStoreTools.Core.Entities
{
    public class Client
    {
        public Client(Guid clientId, int roleId, string passwordHash, string login)
        {
            ClientId = clientId;
            RoleId = roleId;
            PasswordHash = passwordHash;
            Login = login;
        }

        public Client(Guid clientId, int roleId, Role role, string passwordHash, string login)
        {
            ClientId = clientId;
            RoleId = roleId;
            PasswordHash = passwordHash;
            Login = login;
            Role = role;
        }

        public Client()
        {
        }

        [Key]
        [Column("clientid")]
        public Guid ClientId { get; private set; }
        [ForeignKey("Role")]
        [Column("roleid")]
        public int RoleId { get; private set; }
        [Column("password")]
        public string PasswordHash { get; private set; }
        [Column("login")]
        public string Login { get; private set; }

        public Role Role { get;  set; }
    }
}
