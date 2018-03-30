using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreTools.DTO.Entities.Connection
{
    public class InsertConnectionParameterDTO
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool IsDefault { get; set; }
        public int RoleId { get;  set; }
        public string ServerAddress { get;  set; }
    }
}
