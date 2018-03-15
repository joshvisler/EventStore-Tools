using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EventStoreTools.Web.Logger
{
    public class ApplicationLogger : ILogger
    {
        private string filePath;
        private object _lock = new object();

        public ApplicationLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                if (formatter != null)
                {
                    lock (_lock)
                    {
                        File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
