
using System.Data;
using GameServer.Domain.Enums;

namespace GameServer.Contracts.Infrastructure
{
    public interface INamedLockExecutor
    {
        
        Task<T?> ExecuteWithNamedLockAsync<T>(string lockKey, DatabaseType dbType, Func<IDbConnection, Task<T>> action, int timeoutSeconds = 5);
        Task<T?> ExecuteQueryAsync<T>( DatabaseType dbType, Func<IDbConnection, Task<T>> queryAction);
        Task ExecuteQueryAsync(DatabaseType dbType, Func<IDbConnection, Task> queryAction);
        Task<T?> ExecuteCachedQueryAsync<T>(string cacheKey,DatabaseType dbType,Func<IDbConnection, Task<T>> dbQuery,TimeSpan? cacheDuration = null);
    }
}
