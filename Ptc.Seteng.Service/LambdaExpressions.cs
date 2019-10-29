using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public static class LambdaExpressions
    {

        /// <summary>
        /// dynamic include
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IQueryable<T> Include<T>(this ObjectSet<T> source, Expression<Func<T, object>> path) where T : class
        {

            StringBuilder pathBuilder = new StringBuilder();

            MemberExpression pro = path.Body as MemberExpression;
            while (pro != null)
            {
                //Exprssion有點像鏈結串列，從Statemant的後方往前連結，如: x=> x.Customer.CustomerAddress
                //path.Body是CustomerAddress
                //CustomerAddress的Expression是Customer
                //Customer的Expression是x
                pathBuilder.Insert(0, "." + pro.Member.Name);
                pro = pro.Expression as MemberExpression;
            }

            return source.Include(pathBuilder.ToString(1, pathBuilder.Length - 1));
        }

        /// <summary>
        /// dynamic orderby
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="SortProperty"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> Source,  KeyValuePair<string,OrderType> data)
        {

            var type = typeof(T);

            string SortProperty = data.Key;

            OrderType OrderType = data.Value;

            if (string.IsNullOrEmpty(SortProperty))
                return Source;

            var property = type.GetProperty(SortProperty);

            if (property == null)
                return Source;

            var parameter = Expression.Parameter(type, "x");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var typeArguments = new Type[] { type, property.PropertyType };

            var methodName = OrderType == OrderType.Asc ? "OrderBy" : "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, Source.Expression, Expression.Quote(orderByExp));

            return Source.Provider.CreateQuery<T>(resultExp);
        }
        /// <summary>
        /// dynamic thenby
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="SortProperty"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static IQueryable<T> ThenBy<T>(this IQueryable<T> Source, KeyValuePair<string, OrderType> data)
        {
            var type = typeof(T);

            string SortProperty = data.Key;

            OrderType OrderType = data.Value;

            var property = type.GetProperty(SortProperty);

            if (property == null)
                throw new ArgumentNullException("this prop is not exist");

            var parameter = Expression.Parameter(type, "x");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var typeArguments = new Type[] { type, property.PropertyType };

            var methodName = OrderType == OrderType.Asc ? "ThenBy" : "ThenByDescending";

            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, Source.Expression, Expression.Quote(orderByExp));

            return Source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// IEnumerable foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration ?? new List<T>())
            {
                action(item);
            }
        }


        /// <summary>
        /// 取得物件的欄位名稱
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetProperties(this object obj)
        {
            try
            {
                return obj.GetType()
                          .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                          .AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }

        }


        public static IEnumerable<PropertyInfo> GetRecursiveProperties(this object obj)
        {
            try
            {

                return typeof(DataBase.TCMPDAT)
                          .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                          .AsEnumerable()
                          .SelectMany(x=> {
                              var s = x.Name;
                              return x.GetProperties();

                          });
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 取得Attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttributes(typeof(T), true).Cast<T>();
        }
    }
}
