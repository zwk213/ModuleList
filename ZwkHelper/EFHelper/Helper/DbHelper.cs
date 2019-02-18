using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFHelper.Helper
{
    public class ParameterReBinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        public ParameterReBinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
            Expression exp)
        {
            return new ParameterReBinder(map).Visit(exp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (_map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public static class DbHelper
    {
        /// <summary>
        /// 与
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">条件1</param>
        /// <param name="second">条件2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return ParameterReBinder.Compose(first, second, Expression.And);
        }

        /// <summary>
        /// 或
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">条件1</param>
        /// <param name="second">条件2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return ParameterReBinder.Compose(first, second, Expression.Or);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="orderby">createdate desc,id asc</param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string orderby)
        {
            if (string.IsNullOrEmpty(orderby))
                return source;
            string[] orderByArray = orderby.Split(",");

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            Expression queryExpr = source.Expression;
            string methodAsc = "OrderBy";
            string methodDesc = "OrderByDescending";
            foreach (var item in orderByArray)
            {
                string[] order = item.Split(' ');
                bool asc = order.Length == 1 || order[1].ToLower() == "asc";

                MemberExpression property = Expression.Property(parameter, order[0]);
                LambdaExpression lambda = Expression.Lambda(property, parameter);

                queryExpr = Expression.Call(
                    typeof(Queryable),
                    asc ? methodAsc : methodDesc,
                    new Type[] { source.ElementType, property.Type },
                    queryExpr,
                    Expression.Quote(lambda)
                );
                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }
            return source.Provider.CreateQuery<T>(queryExpr);
        }

    }

}
