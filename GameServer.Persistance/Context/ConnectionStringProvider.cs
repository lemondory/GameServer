using GameServer.Contracts.Infrastructure;
using GameServer.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace GameServer.Persistance.Context
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly Dictionary<DatabaseType, string> _connectionStrings;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _connectionStrings = new Dictionary<DatabaseType, string>();

            foreach (var databaseType in (DatabaseType[])Enum.GetValues(typeof(DatabaseType)))
            {
                var connectionString = configuration.GetConnectionString(databaseType.ToString());
                if (!string.IsNullOrEmpty(connectionString))
                {
                    _connectionStrings[databaseType] = connectionString;
                }
                else
                {
                    throw new Exception($"Connection string for '{databaseType}' not found in configuration.");
                }
            }
        }

        public string GetConnectionString(DatabaseType databaseType)
        {
            if (!_connectionStrings.TryGetValue(databaseType, out var connectionString))
            {
                throw new InvalidOperationException($"Connection string for '{databaseType}' not found.");
            }

            return connectionString;
        }
    }
}
