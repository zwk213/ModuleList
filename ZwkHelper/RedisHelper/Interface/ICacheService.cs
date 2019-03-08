using System.Collections.Generic;
using System.Threading.Tasks;
using CacheHelper.Enum;

namespace CacheHelper.Interface
{
    public interface ICacheService
    {
        bool Exists(string key);

        void Add(string key, object value, CacheExpired expired);

        void Remove(string key);

        void Remove(IEnumerable<string> keys);

        object Get(string key);

        T Get<T>(string key) where T : class;

        Task<object> GetAsync(string key);

        Task<T> GetAsync<T>(string key) where T : class;

        Task<bool> ExistsAsync(string key);

        Task AddAsync(string key, object value, CacheExpired expired);

        Task RemoveAsync(string key);

        Task RemoveAsync(IEnumerable<string> keys);
    }
}
