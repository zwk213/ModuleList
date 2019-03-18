using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFHelper.Model;

namespace EFHelper.Interface.Item
{
    public interface ISelect<T> where T : BaseModel
    {
        #region 获取计数与存在

        int Count(Expression<Func<T, bool>> where);
        Task<int> CountAsync(Expression<Func<T, bool>> where);

        bool Exist(Expression<Func<T, bool>> where);
        Task<bool> ExistAsync(Expression<Func<T, bool>> where);

        #endregion

        #region 获取单个

        T Select(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties);
        Task<T> SelectAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties);

        #endregion

        #region 获取部分

        List<T> SelectAny(Expression<Func<T, bool>> where, string orderby, int size, params Expression<Func<T, object>>[] properties);
        Task<List<T>> SelectAnyAsync(Expression<Func<T, bool>> where, string orderby, int size, params Expression<Func<T, object>>[] properties);

        PageData<T> SelectPage(Expression<Func<T, bool>> where, string orderby, int page, int size, params Expression<Func<T, object>>[] properties);
        Task<PageData<T>> SelectPageAsync(Expression<Func<T, bool>> where, string orderby, int page, int size, params Expression<Func<T, object>>[] properties);

        #endregion

        #region 获取指定列

        List<TR> SelectColumns<TR>(Expression<Func<T, bool>> where, Expression<Func<T, TR>> select, string orderby, int page, int size);

        Task<List<TR>> SelectColumnsAsync<TR>(Expression<Func<T, bool>> where, Expression<Func<T, TR>> select, string orderby, int page, int size);

        #endregion

    }
}
