using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EFHelper.Model;

namespace EFHelper.Interface.Item
{
    public interface IUpdate<T> where T : BaseModel
    {
        void Update(T model);
        Task UpdateAsync(T model);
    }
}
