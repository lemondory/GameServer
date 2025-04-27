using GameServer.Domain.Entities;

namespace GameServer.Contracts.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountEntity> CreateAccountAsync(AccountEntity account);
        Task<AccountEntity?> GetAccountByExternalAsync(string externalId, int externalProvider);
        Task<AccountEntity?> GetAccountByCheckRefreshTokenAsync(long AccountUID, string refreshToken);
        Task UpdateLastLoginAndTokenAsync(long accountUid, string token, DateTime? tokenExpiration, DateTime lastLogin);
    }
}