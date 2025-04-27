using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace GameServer.Infrastructure.Logging
{
    public static class SerilogConfigurator
    {
        public static void CreateLogger(IConfiguration configuration)
        {   
            var config = new LoggerConfiguration();
            var serilogSection = configuration.GetSection("Serilog");

            if (serilogSection.Exists())
            {
                Log.Logger = config.ReadFrom.Configuration(configuration).CreateLogger();
            }
            else
            {
                Log.Logger = config
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                    .Enrich.WithProperty("Service", "GameServer")
                    .WriteTo.File("Logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: 1_000_000_000,
                        rollOnFileSizeLimit: true,
                        retainedFileCountLimit: 30,
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(2))
                    .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger();
            }
        }
    }
}
