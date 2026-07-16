using Base.Application.Contracts;
using StackExchange.Redis;

namespace Base.Infrastructure.Implementation
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task SetVerificationCodeAsync(string email, string code, TimeSpan expiration)
        {
            await _db.StringSetAsync($"verification:{email}", code, expiration);
        }

        public async Task<string> GetVerificationCodeAsync(string email)
        {
            return await _db.StringGetAsync($"verification:{email}");
        }

        public async Task RemoveVerificationCodeAsync(string email)
        {
            await _db.KeyDeleteAsync($"verification:{email}");
        }
    }
}