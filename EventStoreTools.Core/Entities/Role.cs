using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventStoreTools.Core.Entities
{
    public class Role
    {
        [Key]
        [Column("roleid")]
        public int RoleId { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
