using EventStore.ClientAPI;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace EventStoreTools.Infrastructure.EventStore.Context
{
    public class EventStoreConnectionContext : IEventStoreConnectionContext
    {
        public IEventStoreConnection Connection { get; private set; }
        //private readonly ILogger _logger;

        public EventStoreConnectionContext(string connectionString/*, ILogger logger*/)
        { 
           // _logger = logger;

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

        private void DisconnectHandler(object o, ClientConnectionEventArgs arg)
        {
           // _logger.LogInformation("Disconnected from EventStore", arg);
            Connect();
        }

        private void ConnectionHandler(object o, ClientConnectionEventArgs arg)
        {
          //  _logger.LogInformation("Connect to EventStore successful", arg);
        }

        private void ReconectionHandler(object o, ClientReconnectingEventArgs arg)
        {
           // _logger.LogInformation("Reconnecting to EventStore", arg);
        }

        private void ConnectionCloseHandler(object o, ClientClosedEventArgs arg)
        {
           // _logger.LogInformation("Connection Closed", arg);
        }

        private void AuthenticationFailedHandler(object o, ClientAuthenticationFailedEventArgs arg)
        {
            //_logger.LogInformation("AuthenticationFailed", arg);
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
