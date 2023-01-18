using LibLite.Inventero.Core.Consts;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LibLite.Inventero.DAL.Extensions
{
    internal static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> query,
            string property,
            string direction)
        {
            return direction == SortDirection.ASC
                ? query.OrderBy(property)
                : query.OrderByDescending(property);
        }

        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> query,
            string property)
        {
            return ApplyOrder(query, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> query,
            string property)
        {
            return ApplyOrder(query, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> query,
            string property)
        {
            return ApplyOrder(query, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> query,
            string property)
        {
            return ApplyOrder(query, property, "ThenByDescending");
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> query,
            string property,
            string methodName)
        {
            var props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                var pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { query, lambda });
            return (IOrderedQueryable<T>)result;
        }

        public static IQueryable<T> SearchBy<T>(this IQueryable<T> query, string searchBy)
        {
            if (string.IsNullOrWhiteSpace(searchBy)) { return query; }
            var searchQuery = CreateSearchQuery<T>();
            return query.Where(searchQuery, searchBy.ToLower());
        }

        private static string CreateSearchQuery<T>()
        {
            var properties = GetProperties<T>();
            var stringBuilder = new StringBuilder();
            foreach (var property in properties)
            {
                stringBuilder.Append($"string(object({property.Name})).ToLower().Contains(@0) OR ");
            }
            var result = stringBuilder.ToString();
            return result[..result.LastIndexOf("OR ")];
        }

        private static IEnumerable<PropertyInfo> GetProperties<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string))
                .ToList();
        }
    }
}
