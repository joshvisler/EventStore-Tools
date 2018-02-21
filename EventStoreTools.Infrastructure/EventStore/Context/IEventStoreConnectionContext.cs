using EventStore.ClientAPI;
using System;

namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public interface IEventStoreConnectionContext : IDisposable
    {
        void Connect();
        IEventStoreConnection Connection { get; }
    }
}
