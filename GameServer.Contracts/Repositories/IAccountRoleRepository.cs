using GameServer.Domain.Entities;

namespace GameServer.Contracts.Repositories
{
    public interface IAccountRoleRepository
    {
        Task<IEnumerable<AccountRole>> GetRolesAsync();
        Task<AccountRole?> GetRoleByIdAsync(int roleId);
        Task AddRoleAsync(AccountRole role);
        Task UpdateRoleAsync(AccountRole role);
        Task DeleteRoleAsync(int roleId);
    }
}