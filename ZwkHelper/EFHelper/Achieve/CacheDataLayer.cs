using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CacheHelper.Achieve;
using CacheHelper.Enum;
using CacheHelper.Interface;
using CoreHelper;
using EFHelper.Helper;
using EFHelper.Interface;
using EFHelper.Interface.Item;
using EFHelper.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFHelper.Achieve
{
    public class CacheDataLayer<T> : ICacheDataLayer<T> where T : BaseModel
    {
        protected DbContext DbContext { get; set; }

        private readonly ICacheService _cacheService;

        public CacheDataLayer(ICacheService service)
        {
            _cacheService = service;
        }

        #region 增加

        public T Insert(T model)
        {
            DbContext.Set<T>().Add(model);
            Commit();
            _cacheService.Add(model.PrimaryKey, JsonHelper.Serialize(model), CacheExpired.Day);
            return model;
        }

        public async Task<T> InsertAsync(T model)
        {
            await DbContext.Set<T>().AddAsync(model);
            await CommitAsync();
            _cacheService.Add(model.PrimaryKey, JsonHelper.Serialize(model), CacheExpired.Day);
            return model;
        }

        public void Insert(T[] models)
        {
            DbContext.Set<T>().AddRange(models);
            Commit();
            foreach (var model in models)
            {
                _cacheService.Add(model.PrimaryKey, JsonHelper.Serialize(model), CacheExpired.Day);
            }
        }

        public async Task InsertAsync(T[] models)
        {
            await DbContext.Set<T>().AddRangeAsync(models);
            await CommitAsync();
            foreach (var model in models)
            {
                await _cacheService.AddAsync(model.PrimaryKey, JsonHelper.Serialize(model), CacheExpired.Day);
            }
        }

        #endregion

        #region 删除

        public void Delete(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            //缓存删除
            _cacheService.Remove(model.PrimaryKey);
            //数据库删除
            temp.State = EntityState.Deleted;
            Commit();
        }

        public async Task DeleteAsync(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            //缓存删除
            await _cacheService.RemoveAsync(model.PrimaryKey);
            //数据库删除
            temp.State = EntityState.Deleted;
            await CommitAsync();
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            var temp = DbContext.Set<T>().Where(where);
            //缓存删除
            foreach (var t in temp)
            {
                _cacheService.Remove(t.PrimaryKey);
            }
            //数据库删除
            DbContext.RemoveRange(temp);
            Commit();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            var temp = DbContext.Set<T>().Where(where);
            //缓存删除
            foreach (var t in temp)
            {
                await _cacheService.RemoveAsync(t.PrimaryKey);
            }
            //数据库删除
            DbContext.RemoveRange(temp);
            await CommitAsync();
        }

        #endregion

        #region 修改

        public void Update(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            //缓存删除
            _cacheService.Remove(model.PrimaryKey);
            //数据库更新
            temp.State = EntityState.Modified;
            Commit();
        }

        public async Task UpdateAsync(T model)
        {
            EntityEntry temp = DbContext.Entry(model);
            //缓存删除
            await _cacheService.RemoveAsync(model.PrimaryKey);
            //数据库更新
            temp.State = EntityState.Modified;
            await CommitAsync();
        }

        #endregion

        #region 计数与存在判断 

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

        public bool Exist(string key)
        {
            var flag = _cacheService.Get(key);
            if (flag != null)
                return true;
            return Exist(p => p.PrimaryKey == key);
        }

        public async Task<bool> ExistAsync(string key)
        {
            var flag = await _cacheService.GetAsync(key);
            if (flag != null)
                return true;
            return await ExistAsync(p => p.PrimaryKey == key);
        }

        #endregion

        #region 获取主键

        public string SelectKey(Expression<Func<T, bool>> where)
        {
            var result = DbContext.Set<T>().FirstOrDefault(where);
            if (result != null)
                return result.PrimaryKey;
            return "";
        }

        public async Task<string> SelectKeyAsync(Expression<Func<T, bool>> where)
        {
            var result = await DbContext.Set<T>().FirstOrDefaultAsync(where);
            if (result != null)
                return result.PrimaryKey;
            return "";
        }

        public List<string> SelectKeys(Expression<Func<T, bool>> where, string orderby, int page, int size)
        {
            var result = DbContext.Set<T>()
                .Where(where)
                .SortBy(orderby)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p => p.PrimaryKey)
                .ToList();
            return result;
        }

        public async Task<List<string>> SelectKeysAsync(Expression<Func<T, bool>> where, string orderby, int page, int size)
        {
            var result = await DbContext.Set<T>()
                .Where(where)
                .SortBy(orderby)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p => p.PrimaryKey)
                .ToListAsync();
            return result;
        }

        #endregion

        #region 获取单个

        /// <summary>
        /// 使用缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Select(string key)
        {
            var result = _cacheService.Get<T>(key);
            if (result != null)
                return result;
            return Select(p => p.PrimaryKey == key);
        }

        /// <summary>
        /// 使用缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> SelectAsync(string key)
        {
            var result = await _cacheService.GetAsync<T>(key);
            if (result != null)
                return result;
            return await SelectAsync(p => p.PrimaryKey == key);
        }

        public T Select(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties)
        {
            var result = Filter(where, properties).AsNoTracking().FirstOrDefault();
            if (result != null)
                _cacheService.Add(result.PrimaryKey, result, CacheExpired.Day);
            return result;
        }

        public async Task<T> SelectAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties)
        {
            return await Task.Run(() => Select(where, properties));
        }

        #endregion

        #region 获取多个

        /// <summary>
        /// 从缓存获取
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="notInCache"></param>
        /// <returns></returns>
        public List<T> SelectAny(List<string> keys, out List<string> notInCache)
        {
            List<T> result = new List<T>();
            notInCache = new List<string>();
            foreach (var key in keys)
            {
                var temp = _cacheService.Get<T>(key);
                if (temp != null)
                    result.Add(temp);
                else
                    notInCache.Add(key);
            }
            return result;
        }

        public List<T> SelectAny(Expression<Func<T, bool>> where, string orderby, int size,
            params Expression<Func<T, object>>[] properties)
        {
            var keys = SelectKeys(where, orderby, 1, size);
            var result = SelectAny(keys, out var not);
            result.AddRange(Filter(p => not.Contains(p.PrimaryKey), properties).ToList());
            return result;
        }

        public async Task<List<T>> SelectAnyAsync(Expression<Func<T, bool>> where, string orderby, int size,
            params Expression<Func<T, object>>[] properties)
        {
            return await Task.Run(() => SelectAny(where, orderby, size, properties));
        }

        #endregion

        #region 获取一页

        public PageData<T> SelectPage(Expression<Func<T, bool>> where, string orderby, int page, int size, params Expression<Func<T, object>>[] properties)
        {
            var keys = SelectKeys(where, orderby, page, size);
            var data = SelectAny(keys, out var not);
            data.AddRange(Filter(p => not.Contains(p.PrimaryKey), properties).ToList());

            PageData<T> result = new PageData<T>()
            {
                Page = page,
                Size = size,
                Count = Count(where),
                Data = data,
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
