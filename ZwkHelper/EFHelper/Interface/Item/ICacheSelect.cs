using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFHelper.Model;

namespace EFHelper.Interface.Item
{
    public interface ICacheSelect<T> : ISelect<T> where T : BaseModel
    {
        bool Exist(string key);

        Task<bool> ExistAsync(string key);

        T Select(string key);

        Task<T> SelectAsync(string key);

        List<T> SelectAny(List<string> keys, out List<string> notInCache);

        string SelectKey(Expression<Func<T, bool>> where);

        Task<string> SelectKeyAsync(Expression<Func<T, bool>> where);

        List<string> SelectKeys(Expression<Func<T, bool>> where, string orderBy, int page, int size);

        Task<List<string>> SelectKeysAsync(Expression<Func<T, bool>> where, string orderBy, int page, int size);

    }
}
