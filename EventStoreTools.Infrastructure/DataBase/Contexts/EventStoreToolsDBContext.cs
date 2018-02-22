using EventStoreTools.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventStoreTools.Infrastructure.DataBase.Contexts
{
    public class EventStoreToolsDBContext:DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }

        public EventStoreToolsDBContext(DbContextOptions<EventStoreToolsDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().ToTable("roles");
            builder.Entity<Client>().ToTable("clients");
            builder.Entity<Connection>().ToTable("connections");
            builder.Entity<Subscribe>().ToTable("subscribes");
        }
    }
}
