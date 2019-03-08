using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFHelper.Helper;
using EFHelper.Interface;
using EFHelper.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFHelper.Achieve
{
    public class DataLayer<T> : IDataLayer<T> where T : BaseModel
    {
        protected DbContext DbContext { get; set; }

        public DataLayer(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        #region 增加

        public T Insert(T model)
        {
            DbContext.Set<T>().Add(model);
            Commit();
            return model;
        }

        public async Task<T> InsertAsync(T model)
        {
            await DbContext.Set<T>().AddAsync(model);
            await CommitAsync();
            return model;
        }

        public void Insert(T[] model)
        {
            DbContext.Set<T>().AddRange(model);
            Commit();
        }

        public async Task InsertAsync(T[] models)
        {
            await DbContext.Set<T>().AddRangeAsync(models);
            await CommitAsync();
        }

        #endregion

        #region 删除

        public void Delete(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            temp.State = EntityState.Deleted;
            Commit();
        }

        public async Task DeleteAsync(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            temp.State = EntityState.Deleted;
            await CommitAsync();
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            var temp = DbContext.Set<T>().Where(where);
            DbContext.RemoveRange(temp);
            Commit();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            var temp = DbContext.Set<T>().Where(where);
            DbContext.RemoveRange(temp);
            await CommitAsync();
        }

        #endregion

        #region 修改

        public void Update(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            temp.State = EntityState.Modified;
            Commit();
        }

        public async Task UpdateAsync(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            temp.State = EntityState.Modified;
            await CommitAsync();
        }

        #endregion

        #region 获取其它数据

        public int Count(Expression<Func<T, bool>> where)
        {
            return DbContext.Set<T>().Count(where);
        }
         
        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await DbContext.Set<T>().CountAsync(where);
        }

        public bool Exist(Expression<Func<T, bool>> where)
        {
            return DbContext.Set<T>().Any(where);
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> where)
        {
            return await DbContext.Set<T>().AnyAsync(where);
        }

        #endregion

        #region 获取单个

        public T Select(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties)
        {
            return Filter(where, properties).AsNoTracking().FirstOrDefault();
        }

        public async Task<T> SelectAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties)
        {
            return await Task.Run(() => Select(where, properties));
        }

        #endregion

        #region 获取多个

        public List<T> SelectAny(Expression<Func<T, bool>> where, string orderby, int size,
            params Expression<Func<T, object>>[] properties)
        {
            return Filter(where, properties).SortBy(orderby).Take(size).ToList();
        }

        public async Task<List<T>> SelectAnyAsync(Expression<Func<T, bool>> where, string orderby, int size,
            params Expression<Func<T, object>>[] properties)
        {
            return await Task.Run(() => SelectAny(where, orderby, size, properties));
        }

        public PageData<T> SelectPage(Expression<Func<T, bool>> where, string orderby, int page, int size, params Expression<Func<T, object>>[] properties)
        {
            PageData<T> result = new PageData<T>()
            {
                Page = page,
                Size = size,
                Count = Count(where),
                Data = Filter(where, properties).SortBy(orderby).Skip((page - 1) * size).Take(size).ToList(),
            };
            return result;
        }

        public async Task<PageData<T>> SelectPageAsync(Expression<Func<T, bool>> where, string orderby, int page, int size, params Expression<Func<T, object>>[] properties)
        {
            return await Task.Run(() => SelectPage(where, orderby, page, size, properties));
        }

        #endregion

        #region 获取部分列

        public List<TR> GetColumns<TR>(Expression<Func<T, bool>> where, Expression<Func<T, TR>> select, string orderby, int page, int size)
        {
            return DbContext.Set<T>()
                .Where(where)
                .Select(select)
                .SortBy(orderby)
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
        }

        public async Task<List<TR>> GetColumnsAsync<TR>(Expression<Func<T, bool>> where, Expression<Func<T, TR>> select, string orderby, int page, int size)
        {
            return await Task.Run(() => GetColumns(where, select, orderby, page, size));
        }

        #endregion

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            var dbSet = query.AsQueryable();
            if (where != null)
            {
                dbSet = dbSet.Where(where).AsNoTracking();
            }
            return dbSet;
        }

    }
}
