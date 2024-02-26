using HRLeaveManagement.Application.Contracts.Logging;
using Microsoft.Extensions.Logging;

namespace HRLeaveManagement.Infrastructure.Logging
{
    public class LoggerAdaptor<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdaptor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
