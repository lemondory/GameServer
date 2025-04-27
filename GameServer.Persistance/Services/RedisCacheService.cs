using GameServer.Contracts.Infrastructure;
using GameServer.Contracts.Services;
using StackExchange.Redis;

namespace GameServer.Persistance.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _database;
        private readonly ISerializer _serializer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
        {
            _database = connectionMultiplexer.GetDatabase();
            _serializer = serializer;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }

            return _serializer.Deserialize<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration)
        {
            var serializedValue = _serializer.Serialize(value);
            await _database.StringSetAsync(key, serializedValue, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}