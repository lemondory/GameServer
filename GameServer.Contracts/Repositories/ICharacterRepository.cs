using GameServer.Domain.Entities;

namespace GameServer.Contracts.Repositories
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<CharacterEntity>> GetByAccountUIDAsync(long accountUID);
        Task<int> CountByAccountAndServerAsync(long accountUID, int serverId);
        Task CreateAsync(CharacterEntity character);
    }
}
