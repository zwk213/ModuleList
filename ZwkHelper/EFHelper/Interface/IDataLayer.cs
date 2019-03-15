using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFHelper.Interface.Item;
using EFHelper.Model;
using Microsoft.EntityFrameworkCore;

namespace EFHelper.Interface
{
    public interface IDataLayer<T> : IInsert<T>, IUpdate<T>, IDelete<T>, ISelect<T> where T : BaseModel
    {
        DbContext DbContext { get; set; }

        IQueryable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties);

        void Commit();

        Task CommitAsync();
    }

}
