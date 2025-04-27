using GameServer.Domain.Enums;

namespace GameServer.Contracts.Infrastructure
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(DatabaseType databaseType);
    }
}