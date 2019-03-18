using System.Collections.Generic;
using System.Threading.Tasks;
using CacheHelper.Enum;

namespace CacheHelper.Interface
{
    public interface ICacheService
    {
        void Add(string key, object value, CacheExpired expired);

        void Remove(string key);

        void Remove(IEnumerable<string> keys);

        string Get(string key);

        T Get<T>(string key) where T : class;

        Task<object> GetAsync(string key);

        Task<T> GetAsync<T>(string key) where T : class;

        Task AddAsync(string key, object value, CacheExpired expired);

        Task RemoveAsync(string key);

        Task RemoveAsync(IEnumerable<string> keys);
    }
}
