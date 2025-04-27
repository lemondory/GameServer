using GameServer.Domain.Entities;

namespace GameServer.Contracts.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryEntity>> GetInventoryAsync(int inventoryId);
    }
}