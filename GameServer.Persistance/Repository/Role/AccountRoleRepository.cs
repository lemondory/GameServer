using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Repositories;
using GameServer.Domain.Entities;

public class AccountRoleRepository : IAccountRoleRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly INamedLockExecutor _namedLockExecutor;
    private readonly IRedisCacheService _cacheService;
    public AccountRoleRepository(IDbConnectionFactory dbConnectionFactory, INamedLockExecutor namedLockExecutor, IRedisCacheService cacheService)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _namedLockExecutor = namedLockExecutor;
        _cacheService = cacheService;
    }
    public async Task<IEnumerable<AccountRole>> GetRolesAsync()
    {
        //using var connection = await _context.GetConnectionAsync(DatabaseType.Account);
        //var query = "SELECT Id, Name FROM AccountRole";
        //return await connection.QueryAsync<AccountRole>(query);
        return null;
    }

    public async Task<AccountRole?> GetRoleByIdAsync(int roleId)
    {
        //using var connection = await _context.GetConnectionAsync(DatabaseType.Account);
        //var query = "SELECT Id, Name FROM AccountRole WHERE Id = @RoleId";
        //return await connection.QueryFirstOrDefaultAsync<AccountRole>(query, new { RoleId = roleId });
        return null;
    }

    public async Task AddRoleAsync(AccountRole role)
    {
        //using var connection = await _context.GetConnectionAsync(DatabaseType.Account);
        //var query = "INSERT INTO AccountRole (Name) VALUES (@Name)";
        //await connection.ExecuteAsync(query, new { role.Name });
    }

    public async Task UpdateRoleAsync(AccountRole role)
    {
        //using var connection = await _context.GetConnectionAsync(DatabaseType.Account);
        //var query = "UPDATE AccountRole SET Name = @Name WHERE Id = @Id";
        //await connection.ExecuteAsync(query, new { role.Name, role.Id });
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        //using var connection = await _context.GetConnectionAsync(DatabaseType.Account);
        //var query = "DELETE FROM AccountRole WHERE Id = @RoleId";
        //await connection.ExecuteAsync(query, new { RoleId = roleId });
    }
}
