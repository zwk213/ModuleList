using System.Collections.Generic;
using System.Threading.Tasks;
using CacheHelper.Enum;
using CacheHelper.Interface;
using JsonHelper;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheHelper.Achieve
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public string Get(string key)
        {
            return _cache.GetString(key);
        }

        public T Get<T>(string key) where T : class
        {
            var temp = _cache.GetString(key);
            if (string.IsNullOrEmpty(temp))
                return null;
            return Json.Deserialize<T>(temp);
        }

        public void Add(string key, object value, CacheExpired expired)
        {
            if (!string.IsNullOrEmpty(Get(key)))
                return;
            _cache.SetString(key, Json.Serialize(value), new DistributedCacheEntryOptions { SlidingExpiration = expired.ToTimeSpan() });
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Remove(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                Remove(key);
            }
        }

        public async Task<object> GetAsync(string key)
        {
            return await Task.Run(() => Get(key));
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            return await Task.Run(() => Get<T>(key));
        }

        public async Task AddAsync(string key, object value, CacheExpired expired)
        {
            await Task.Run(() => Add(key, value, expired));
        }

        public async Task RemoveAsync(string key)
        {
            await Task.Run(() => Remove(key));
        }

        public async Task RemoveAsync(IEnumerable<string> keys)
        {
            await Task.Run(() => Remove(keys));
        }

    }
}
