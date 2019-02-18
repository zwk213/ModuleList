using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFHelper.Model;

namespace EFHelper.Interface.Item
{
    public interface IDelete<T> where T : BaseModel
    {
        void Delete(T model);
        Task DeleteAsync(T model);

        void Delete(Expression<Func<T, bool>> where);
        Task DeleteAsync(Expression<Func<T, bool>> where);
    }
}
