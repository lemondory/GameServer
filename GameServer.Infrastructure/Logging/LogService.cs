using GameServer.Contracts.Services;
using Serilog;


namespace GameServer.Infrastructure.Logging
{
    public class LogService : ILogService
    {
        public void Info(string message) => Log.Information(message);
        public void Warning(string message) => Log.Warning(message);
        public void Error(string message, Exception? ex = null)
        {
            if (ex != null)
                Log.Error(ex, message);
            else
                Log.Error(message);
        }
        public void Error(string messageTemplate, Exception ex, params object[] args)
        {
            if (ex != null)
                Log.Error(ex, messageTemplate, args);
            else
                Log.Error(messageTemplate, args);
        }
    }
}
