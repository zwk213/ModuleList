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

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public T Get<T>(string key) where T : class
        {

            return Json.Deserialize<T>(_cache.GetString(key));
        }

        public bool Exists(string key)
        {
            return !string.IsNullOrEmpty(_cache.GetString(key));
        }

        public void Add(string key, object value, CacheExpired expired)
        {
            if (Exists(key))
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

        public async Task<bool> ExistsAsync(string key)
        {
            return await Task.Run(() => Exists(key));
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
