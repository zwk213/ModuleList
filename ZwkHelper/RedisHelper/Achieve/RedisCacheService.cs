using System.Collections.Generic;
using System.Threading.Tasks;
using CoreHelper;
using Microsoft.Extensions.Caching.Distributed;
using RedisHelper.Enum;
using RedisHelper.Interface;

namespace RedisHelper.Achieve
{
    public class RedisService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
            return JsonHelper.Deserialize<T>(_cache.GetString(key));
        }

        public bool Exists(string key)
        {
            return !string.IsNullOrEmpty(_cache.GetString(key));
        }

        public void Add(string key, object value, RedisExpired expired)
        {
            if (Exists(key))
                return;
            _cache.SetString(key, JsonHelper.Serialize(value), new DistributedCacheEntryOptions { SlidingExpiration = expired.ToTimeSpan() });
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

        public async Task AddAsync(string key, object value, RedisExpired expired)
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
