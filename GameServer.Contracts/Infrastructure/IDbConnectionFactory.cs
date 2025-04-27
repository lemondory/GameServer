using GameServer.Domain.Enums;
using MySqlConnector;

namespace GameServer.Contracts.Infrastructure
{
    public interface IDbConnectionFactory
    {
        Task<MySqlConnection> CreateConnectionAsync(DatabaseType dbType);
    }
}
