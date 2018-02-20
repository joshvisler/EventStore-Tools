using EventStore.ClientAPI;
using System;

namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public class EventStoreConnectionContext : IDisposable
    {
        public IEventStoreConnection Connection { get; private set; }
        private readonly ILogger _logger;

        public EventStoreConnectionContext(string connectionString, ILogger logger)
        { 
            _logger = logger;

            var coonectionSettings = ConnectionSettings.Create();
            var connectionBuilder = coonectionSettings.KeepReconnecting();
            connectionBuilder.Build();
            Connection = EventStoreConnection.Create(connectionString, connectionBuilder);
            Connection.Disconnected += DisconnectHandler;
            Connection.Reconnecting += ReconectionHandler;
            Connection.Closed += ConnectionCloseHandler;
            Connection.AuthenticationFailed += AuthenticationFailedHandler;
        }

        public void Connect()
        {
            Connection.ConnectAsync().Wait();
        }

        public void DisconnectHandler(object o, ClientConnectionEventArgs arg)
        {
            _logger.Info("Disconnected from EventStore", arg);
            Connect();
        }

        public void ConnectionHandler(object o, ClientConnectionEventArgs arg)
        {
            _logger.Info("Connect to EventStore successful", arg);
        }

        public void ReconectionHandler(object o, ClientReconnectingEventArgs arg)
        {
            _logger.Info("Reconnecting to EventStore", arg);
        }

        public void ConnectionCloseHandler(object o, ClientClosedEventArgs arg)
        {
            _logger.Info("Connection Closed", arg);
        }

        public void AuthenticationFailedHandler(object o, ClientAuthenticationFailedEventArgs arg)
        {
            _logger.Info("AuthenticationFailed", arg);
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Disconnected -= DisconnectHandler;
            Connection.Reconnecting -= ReconectionHandler;
            Connection.Closed -= ConnectionCloseHandler;
            Connection.AuthenticationFailed -= AuthenticationFailedHandler;
            Connection.Dispose();
        }
    }
}
