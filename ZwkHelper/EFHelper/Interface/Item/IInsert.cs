using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EFHelper.Model;

namespace EFHelper.Interface.Item
{
    public interface IInsert<T> where T:BaseModel
    {
        T Insert(T model);
        Task<T> InsertAsync(T model);

        void Insert(T[] model);

        Task InsertAsync(T[] model);

    }
}
