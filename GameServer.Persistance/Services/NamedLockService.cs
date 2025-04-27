using Dapper;
using GameServer.Contracts.Infrastructure;
using MySqlConnector;

namespace GameServer.Persistance.Services
{
    public class NamedLockService : INamedLockService
    {
        public async Task<bool> AcquireLockAsync(MySqlConnection connection, string lockName, int timeoutSeconds)
        {
            var result = await connection.ExecuteScalarAsync<int>("SELECT GET_LOCK(@lockName, @timeout)",
                new { lockName, timeout = timeoutSeconds });
            return result == 1;
        }

        public async Task<bool> ReleaseLockAsync(MySqlConnection connection, string lockName)
        {
            var result = await connection.ExecuteScalarAsync<int>("SELECT RELEASE_LOCK(@lockName)", new { lockName });
            return result == 1;
        }
    }
}
