using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Entities;

namespace GameServer.Persistance.Repository.Inventory
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly INamedLockExecutor _namedLockExecutor;
        private readonly IRedisCacheService _cacheService;
        public InventoryRepository(IDbConnectionFactory dbConnectionFactory, INamedLockExecutor namedLockExecutor, IRedisCacheService cacheService)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _namedLockExecutor = namedLockExecutor;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<InventoryEntity>> GetInventoryAsync(int inventoryId)
        {
            //using var connection = await _context.GetConnectionAsync(DatabaseType.Account);
            //var query = "SELECT Id, Name FROM Inventory WHERE Id = @InventoryID";
            //return await connection.QueryAsync<InventoryEntity>(query, new { InventoryID = inventoryId });
            return null;
        }
    }
}