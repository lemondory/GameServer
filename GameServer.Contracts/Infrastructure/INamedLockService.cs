using MySqlConnector;

namespace GameServer.Contracts.Infrastructure
{
    public interface INamedLockService
    {
        Task<bool> AcquireLockAsync(MySqlConnection connection, string lockName, int timeoutSeconds);
        Task<bool> ReleaseLockAsync(MySqlConnection connection, string lockName);
    }
}