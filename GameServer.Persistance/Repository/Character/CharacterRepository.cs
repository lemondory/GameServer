using Dapper;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Entities;
using GameServer.Domain.Enums;

namespace GameServer.Persistance.Repositories.Character
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly INamedLockExecutor _namedLockExecutor;
        private readonly IRedisCacheService _cacheService;

        public CharacterRepository(IDbConnectionFactory dbConnectionFactory, INamedLockExecutor namedLockExecutor, IRedisCacheService cacheService)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _namedLockExecutor = namedLockExecutor;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<CharacterEntity>> GetByAccountUIDAsync(long accountUID)
        {
            string cacheKey = $"CharacterList:{accountUID}";

            var cached = await _cacheService.GetAsync<List<CharacterEntity>>(cacheKey);
            if (cached != null)
                return cached;

            var characters = await _namedLockExecutor.ExecuteWithNamedLockAsync(
                cacheKey,
                DatabaseType.Account,
                async conn =>
                {
                    const string sql = @"
                        SELECT 
                            character_uid AS CharacterUID,
                            account_uid AS AccountUID,
                            character_name AS CharacterName,
                            character_level AS CharacterLevel,
                            exp,
                            server_id AS ServerId,
                            created_at AS CreatedAt
                        FROM `Character`
                        WHERE account_uid = @AccountUID";

                    var result = (await conn.QueryAsync<CharacterEntity>(sql, new { AccountUID = accountUID })).ToList();
                    return result;
                });

            if (characters != null)
            {
                await _cacheService.SetAsync(cacheKey, characters, TimeSpan.FromMinutes(10));
            }

            return characters;
        }

        public async Task<int> CountByAccountAndServerAsync(long accountUID, int serverId)
        {
            string lockKey = $"CharacterCount:{accountUID}:{serverId}";

            return await _namedLockExecutor.ExecuteWithNamedLockAsync(
                lockKey,
                DatabaseType.Account,
                async conn =>
                {
                    const string sql = @"
                        SELECT COUNT(*) FROM `Character`
                        WHERE account_uid = @AccountUID AND server_id = @ServerId";

                    return await conn.ExecuteScalarAsync<int>(sql, new { AccountUID = accountUID, ServerId = serverId });
                });
        }

        public async Task CreateAsync(CharacterEntity character)
        {
            string lockKey = $"CharacterCreate:{character.AccountUID}:{character.ServerId}";

            await _namedLockExecutor.ExecuteWithNamedLockAsync(
                lockKey,
                DatabaseType.Account,
                async conn =>
                {
                    const string sql = @"
                        INSERT INTO `Character` (
                            character_uid, account_uid, character_name,
                            character_level, exp, server_id, created_at
                        ) VALUES (
                            @CharacterUID, @AccountUID, @CharacterName,
                            @CharacterLevel, @Exp, @ServerId, @CreatedAt
                        )";

                    await conn.ExecuteAsync(sql, character);
                    return true;
                });

            // invalidate 캐시
            string listCacheKey = $"CharacterList:{character.AccountUID}";
            await _cacheService.RemoveAsync(listCacheKey);
        }
    }
}
