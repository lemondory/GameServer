using Dapper;
using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Entities;
using GameServer.Domain.Enums;
using System.Data;
using Dapper.FastCrud;

namespace GameServer.Persistance.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly INamedLockExecutor _namedLockExecutor;
        private readonly IRedisCacheService _cacheService;

        public AccountRepository(IDbConnectionFactory dbConnectionFactory, INamedLockExecutor namedLockExecutor, IRedisCacheService cacheService)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _namedLockExecutor = namedLockExecutor;
            _cacheService = cacheService;
        }

        public async Task<AccountEntity?> CreateAccountAsync(AccountEntity account)
        {
            return await _namedLockExecutor.ExecuteWithNamedLockAsync(
                lockKey: $"lock:account:create:{account.ExternalId}",  // 적절한 유니크 락키 설정
                dbType: DatabaseType.Account,
                action: async connection =>
                {
                    using var transaction = connection.BeginTransaction();

                    try
                    {
                        await connection.InsertAsync(account, stmt => stmt.AttachToTransaction(transaction));

                        var inserted = await connection.GetAsync<AccountEntity>(
                            account,
                            stmt => stmt.AttachToTransaction(transaction)
                        );

                        transaction.Commit();
                        return inserted;
                    }
                    catch
                    {
                        transaction.Rollback();

                        // TODO LOG
                        throw;
                    }
                }
            );
        }

        public async Task<bool> UpdateAccountAsync(AccountEntity account)
        {
            var result = await _namedLockExecutor.ExecuteWithNamedLockAsync<bool?>(
                lockKey: $"lock:account:update:{account.AccountUid}",
                dbType: DatabaseType.Account,
                action: async connection =>
                {
                    return await connection.UpdateAsync(account);
                }
            );

            return result ?? false;
        }

        public async Task<bool> DeleteAccountAsync(long accountUid)
        {
            var result = await _namedLockExecutor.ExecuteWithNamedLockAsync<bool?>(
                lockKey: $"lock:account:delete:{accountUid}",
                dbType: DatabaseType.Account,
                action: async connection =>
                {
                    var account = new AccountEntity { AccountUid = accountUid };
                    return await connection.DeleteAsync(account);
                }
            );

            return result ?? false;
        }

        public async Task<AccountEntity?> GetByExternalIdAsync(string externalId, byte provider)
        {
            return await _namedLockExecutor.ExecuteQueryAsync(
                dbType: DatabaseType.Account,
                queryAction: async connection =>
                {
                    var results = await connection.FindAsync<AccountEntity>(stmt =>
                        stmt
                            .Where($"{nameof(AccountEntity.ExternalId):C} = @ExternalId AND {nameof(AccountEntity.ExternalProvider):C} = @Provider")   //  :C는 `"external_id"` 또는 `"[ExternalId]"` 또는 ```"ExternalId"``` 처럼 DB 타입에 따라 적절한 형태로 바뀜
                            .WithParameters(new { ExternalId = externalId, Provider = provider })
                    );

                    return results.FirstOrDefault();
                }
            );
        }

        public async Task<AccountEntity?> GetAccountByExternalAsync(string externalId, int externalProvider)
        {
            string cacheKey = $"Account:{externalId}:{externalProvider}";

            return await _namedLockExecutor.ExecuteCachedQueryAsync(
                cacheKey,
                DatabaseType.Account,
                async conn =>
                {
                    var results = await conn.FindAsync<AccountEntity>(stmt =>
                        stmt
                            .Where($"{nameof(AccountEntity.ExternalId):C} = @ExternalId AND {nameof(AccountEntity.ExternalProvider):C} = @ExternalProvider")
                            .WithParameters(new
                            {
                                ExternalId = externalId,
                                ExternalProvider = externalProvider
                            })
                    );

                    return results.FirstOrDefault();
                },
                TimeSpan.FromMinutes(30));
        }

        public async Task<AccountEntity?> GetAccountByCheckRefreshTokenAsync(long accountUID, string refreshToken)
        {
            string cacheKey = $"Account:{accountUID}";

            return await _namedLockExecutor.ExecuteCachedQueryAsync(
                cacheKey,
                DatabaseType.Account,
                async conn =>
                {
                    var results = await conn.FindAsync<AccountEntity>(stmt =>
                        stmt
                            .Where($"{nameof(AccountEntity.AccountUid):C} = @AccountUID AND {nameof(AccountEntity.RefreshToken):C} = @RefreshToken")
                            .WithParameters(new
                            {
                                AccountUID = accountUID,
                                RefreshToken = refreshToken
                            })
                    );

                    return results.FirstOrDefault();
                },
                TimeSpan.FromMinutes(30));
        }

        public async Task UpdateTokenTransactionAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin)
        {
            await _namedLockExecutor.ExecuteWithNamedLockAsync(
                lockKey: $"lock:account:token:update:{accountUid}",
                dbType: DatabaseType.Account,
                action: async conn =>
                {
                    using var transaction = conn.BeginTransaction();

                    try
                    {
                        var account = await conn.GetAsync(new AccountEntity { AccountUid = accountUid }, stmt => stmt.AttachToTransaction(transaction));
                        if (account == null)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        account.RefreshToken = token;
                        account.RefreshTokenExpiration = tokenExpiration;
                        account.LastLoginAt = lastLogin;

                        var success = await conn.UpdateAsync(account, stmt => stmt.AttachToTransaction(transaction));

                        if (!success)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                });
        }

        public async Task DeleteTokenTransactionAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin)
        {
            await _namedLockExecutor.ExecuteWithNamedLockAsync(
                lockKey: $"lock:account:token:delete:{accountUid}",
                dbType: DatabaseType.Account,
                action: async conn =>
                {
                    using var transaction = conn.BeginTransaction();

                    try
                    {
                        var account = await conn.GetAsync(new AccountEntity { AccountUid = accountUid }, stmt => stmt.AttachToTransaction(transaction));
                        if (account == null)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        account.RefreshToken = token;
                        account.RefreshTokenExpiration = tokenExpiration;
                        account.LastLoginAt = lastLogin;

                        var success = await conn.UpdateAsync(account, stmt => stmt.AttachToTransaction(transaction));

                        if (!success)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                });
        }

        public async Task UpdateLastLoginAndTokenAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin)
        {
            await _namedLockExecutor.ExecuteWithNamedLockAsync(
                lockKey: $"lock:account:token:update:{accountUid}",
                dbType: DatabaseType.Account,
                action: async conn =>
                {
                    var account = await conn.GetAsync(new AccountEntity { AccountUid = accountUid });
                    if (account == null)
                        return false;

                    account.RefreshToken = token;
                    account.RefreshTokenExpiration = tokenExpiration;
                    account.LastLoginAt = lastLogin;

                    return await conn.UpdateAsync(account);
                });
        }

        //public async Task<AccountEntity> CreateAccountAsync(AccountEntity account)
        //{
        //    const string insertQuery = @"
        //        INSERT INTO Account (
        //            account_name, external_provider, external_id, 
        //            refresh_token, refresh_token_expiration
        //        ) VALUES (
        //            @AccountName, @ExternalProvider, @ExternalId, 
        //            @RefreshToken, @RefreshTokenExpiration
        //        );
        //        SELECT LAST_INSERT_ID();
        //    ";

        //    const string selectQuery = @"
        //        SELECT 
        //            account_uid AS AccountUID,
        //            account_name AS AccountName,
        //            account_level AS AccountLevel,
        //            external_provider AS ExternalProvider,
        //            external_id AS ExternalId,
        //            ban_type AS BanType,
        //            ban_end_at AS BanEndAt,
        //            refresh_token AS RefreshToken,
        //            refresh_token_expiration AS RefreshTokenExpiration
        //        FROM Account
        //        WHERE account_uid = @AccountUID;
        //    ";

        //    using var connection = await _dbConnectionFactory.CreateConnectionAsync(DatabaseType.Account);

        //    // 트랜잭션 시작 (예외 대비용)
        //    using var transaction = connection.BeginTransaction();

        //    try
        //    {
        //        var accountUid = await connection.ExecuteScalarAsync<long>(
        //            insertQuery, account, transaction);

        //        var created = await connection.QuerySingleOrDefaultAsync<AccountEntity>(
        //            selectQuery, new { AccountUID = accountUid }, transaction);

        //        transaction.Commit();
        //        return created!;
        //    }
        //    catch (Exception ex)
        //    {
        //        // 예외 발생 시 트랜잭션 롤백
        //        transaction.Rollback();
        //        //TODO LOG
        //        return null;
        //    }
        //}

        //public async Task<AccountEntity?> GetAccountByCheckRefreshTokenAsync(long accountUID, string refreshToken)
        //{
        //    string cacheKey = $"Account:{accountUID}";

        //    return await _namedLockExecutor.ExecuteCachedQueryAsync(
        //        cacheKey,
        //        DatabaseType.Account,
        //        async conn =>
        //        {
        //            const string query = @"
        //                SELECT 
        //                    account_uid AS AccountUID,
        //                    account_name AS AccountName,
        //                    account_level AS AccountLevel,
        //                    external_provider AS ExternalProvider,
        //                    external_id AS ExternalId,
        //                    ban_type AS BanType,
        //                    ban_end_at AS BanEndAt,
        //                    account_role_id AS AccountRoleId,
        //                    refresh_token AS RefreshToken,
        //                    refresh_token_expiration AS RefreshTokenExpiration
        //                FROM Account
        //                WHERE account_uid = @AccountUID AND refresh_token = @RefreshToken";

        //            return await conn.QueryFirstOrDefaultAsync<AccountEntity>(
        //                query, new { AccountUID = accountUID, RefreshToken = refreshToken });
        //        },
        //        TimeSpan.FromMinutes(30));
        //}

        //public async Task<AccountEntity?> GetAccountByExternalAsync(string externalId, int externalProvider)
        //{
        //    string cacheKey = $"Account:{externalId}:{externalProvider}";

        //    return await _namedLockExecutor.ExecuteCachedQueryAsync(
        //        cacheKey,
        //        DatabaseType.Account,
        //        async conn =>
        //        {
        //            const string query = @"
        //        SELECT 
        //            account_uid AS AccountUID,
        //            account_name AS AccountName,
        //            account_level AS AccountLevel,
        //            external_provider AS ExternalProvider,
        //            external_id AS ExternalId,
        //            ban_type AS BanType,
        //            ban_end_at AS BanEndAt,
        //            account_role_id AS AccountRoleId,
        //            refresh_token AS RefreshToken,
        //            refresh_token_expiration AS RefreshTokenExpiration
        //        FROM Account
        //        WHERE external_id = @ExternalId AND external_provider = @ExternalProvider";

        //            return await conn.QueryFirstOrDefaultAsync<AccountEntity>(
        //                query, new { ExternalId = externalId, ExternalProvider = externalProvider });
        //        },
        //        TimeSpan.FromMinutes(30));
        //}

        //public async Task UpdateLastLoginAndTokenAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin)
        //{
        //    const string query = @"
        //        UPDATE Account
        //        SET 
        //            refresh_token = @Token,
        //            refresh_token_expiration = @TokenExpiration,
        //            last_login_at = @LastLogin
        //        WHERE account_uid = @AccountUID";

        //    using var connection = await _dbConnectionFactory.CreateConnectionAsync(DatabaseType.Account);
        //    await connection.ExecuteAsync(query, new 
        //    { 
        //        AccountUID = accountUid, 
        //        Token = token, 
        //        TokenExpiration = tokenExpiration, 
        //        LastLogin = lastLogin 
        //    });
        //}

        //public async Task UpdateTokenTransctionAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin)
        //{
        //    const string query = @"
        //        UPDATE Account
        //        SET 
        //            refresh_token = @Token,
        //            refresh_token_expiration = @TokenExpiration,
        //            last_login_at = @LastLogin
        //        WHERE account_uid = @AccountUID";

        //    using var connection = await _dbConnectionFactory.CreateConnectionAsync(DatabaseType.Account);
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        await connection.OpenAsync();
        //    }

        //    using var transaction = connection.BeginTransaction();
        //    if (transaction == null)
        //    {
        //        throw new InvalidOperationException("Transaction is null");
        //    }

        //    try
        //    {
        //        var rowCount = await connection.ExecuteAsync(query, new
        //        {
        //            AccountUID = accountUid,
        //            Token = token,
        //            TokenExpiration = tokenExpiration,
        //            LastLogin = lastLogin
        //        }, transaction);

        //        // 변경된 행이 없으면 롤백    
        //        if (rowCount == 0)
        //        {
        //            transaction.Rollback();
        //        }

        //        // 변경된 행이 있으면 커밋
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        //TODO LOG
        //        throw;
        //    }
        //}

        //public async Task DeleteTokenTransctionAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin)
        //{
        //    const string query = @"
        //        UPDATE Account
        //        SET 
        //            refresh_token = @Token,
        //            refresh_token_expiration = @TokenExpiration,
        //            last_login_at = @LastLogin
        //        WHERE account_uid = @AccountUID";

        //    using var connection = await _dbConnectionFactory.CreateConnectionAsync(DatabaseType.Account);
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        await connection.OpenAsync();
        //    }

        //    using var transaction = connection.BeginTransaction();
        //    if (transaction == null)
        //    {
        //        throw new InvalidOperationException("Transaction is null");
        //    }

        //    try
        //    {
        //        var rowCount = await connection.ExecuteAsync(query, new
        //        {
        //            AccountUID = accountUid,
        //            Token = token,
        //            TokenExpiration = tokenExpiration,
        //            LastLogin = lastLogin
        //        }, transaction);

        //        // 변경된 행이 없으면 롤백    
        //        if (rowCount == 0)
        //        {
        //            transaction.Rollback();
        //        }

        //        // 변경된 행이 있으면 커밋
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        //TODO LOG
        //        throw;
        //    }
        //}

    }
}