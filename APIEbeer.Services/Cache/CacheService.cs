using Microsoft.Extensions.Caching.Memory;

namespace APIEbeer.Services.Cache
{
    public class CacheService(IMemoryCache cache) : ICacheService
    {
        private readonly IMemoryCache _cache = cache;
        private readonly MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3), // Set absolute expiration to 2 hours
            SlidingExpiration = TimeSpan.FromHours(2) // Set sliding expiration to 1 hour
        };
            

        public void Set<T>(string key, T value) where T : class
        {
            _cache.Set(key, value, _cacheOptions);
        }

        public T? Get<T>(string key) where T : class
        {
            return _cache.TryGetValue(key, out T? result) ? result : default;
        }
    }
}
