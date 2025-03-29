using StackExchange.Redis;
using System.Threading.Tasks;

public class RedisCacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    // Set a value in Redis
    public async Task SetCacheValueAsync(string key, string value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value);
    }

    // Get a value from Redis
    public async Task<string> GetCacheValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    // Remove a value from Redis
    public async Task RemoveCacheValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.KeyDeleteAsync(key);
    }
}