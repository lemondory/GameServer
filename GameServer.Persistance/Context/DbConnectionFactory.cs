using Dapper.FastCrud;
using GameServer.Contracts.Infrastructure;
using GameServer.Domain.Enums;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace GameServer.Persistance.Context
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<MySqlConnection> CreateConnectionAsync(DatabaseType dbType)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString(dbType.ToString());

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException($"Connection string for database '{dbType}' is not configured.");

                // 👉 FastCrud에 MySQL 사용을 명시적으로 지정 DB Column Name [], `` 사용에 대해서 처리가 되기 때문에 DB 변경 시 변경 필수
                OrmConfiguration.DefaultDialect = SqlDialect.MySql;

                var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                // 로깅 시스템에 예외 기록
                Console.WriteLine($"[DB CONNECTION ERROR] [{dbType}] {ex.Message}");

                // 필요 시, 커스텀 예외로 래핑해서 던질 수 있음
                throw new Exception($"Failed to create DB connection for {dbType}", ex);
            }
        }
    }
}
