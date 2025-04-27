using GameServer.Contracts.Infrastructure;
using GameServer.Domain.Enums;
using System.Data;

namespace GameServer.Persistance.Services
{
    public class NamedLockExecutor : INamedLockExecutor
    {
        private readonly INamedLockService _namedLockService;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IRedisCacheService _cacheService;

        public NamedLockExecutor(INamedLockService namedLockService, IDbConnectionFactory dbConnectionFactory, IRedisCacheService cacheService)
        {
            _namedLockService = namedLockService;
            _dbConnectionFactory = dbConnectionFactory;
            _cacheService = cacheService;
        }

        public async Task<T?> ExecuteWithNamedLockAsync<T>(
            string lockKey,
            DatabaseType dbType,
            Func<IDbConnection, Task<T>> action,
            int timeoutSeconds = 5)
        {
            await using var connection = await _dbConnectionFactory.CreateConnectionAsync(dbType);

            bool acquired = await _namedLockService.AcquireLockAsync(connection, lockKey, timeoutSeconds);
            if (!acquired)
                return default;

            try
            {
                return await action(connection);
            }
            finally
            {
                await _namedLockService.ReleaseLockAsync(connection, lockKey);
            }
        }

        public async Task<T?> ExecuteQueryAsync<T>(
            DatabaseType dbType,
            Func<IDbConnection, Task<T>> queryAction)
        {
            await using var connection = await _dbConnectionFactory.CreateConnectionAsync(dbType);
            return await queryAction(connection);
        }

        public async Task ExecuteQueryAsync(
            DatabaseType dbType,
            Func<IDbConnection, Task> queryAction)
        {
            await using var connection = await _dbConnectionFactory.CreateConnectionAsync(dbType);
            await queryAction(connection);
        }

        public async Task<T?> ExecuteCachedQueryAsync<T>(string cacheKey,DatabaseType dbType,Func<IDbConnection, Task<T>> dbQuery,TimeSpan? cacheDuration = null)
        {
            // 1. 캐시 조회
            var cached = await _cacheService.GetAsync<T>(cacheKey);
            if (cached != null)
                return cached;

            // 2. 캐시 없으면 DB 조회
            await using var connection = await _dbConnectionFactory.CreateConnectionAsync(dbType);
            var result = await dbQuery(connection);

            // 3. 캐시에 저장 (기본 10분)
            if (result != null)
            {
                await _cacheService.SetAsync(cacheKey, result, cacheDuration ?? TimeSpan.FromMinutes(10));
            }

            return result;
        }
    }
}