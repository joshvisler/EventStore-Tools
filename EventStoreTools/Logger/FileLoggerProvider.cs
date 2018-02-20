using Microsoft.Extensions.Logging;

namespace EventStoreTools.Web.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string path;
        public FileLoggerProvider(string _path)
        {
            path = _path;
        }
        public ILogger CreateLogger(string categoryName) => new ApplicationLogger(path);

        public void Dispose()
        {
        }
    }
}
