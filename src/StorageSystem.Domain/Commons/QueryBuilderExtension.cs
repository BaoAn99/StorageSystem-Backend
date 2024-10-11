using StorageSystem.Domain.Commons;
using System.Linq.Expressions;
using System.Reflection;

namespace storagesystem.domain.commons
{
    public static class QueryBuilderExtension
    {
        // Get All Without Paging
        public static IQueryable<TEntity> BuildQueryWithoutPaging<TEntity>(this IQueryable<TEntity> query, QueryParamsWithoutPaging queryparams)
        {
            return query.BuildQuery(queryparams?.Filters).BuildQuery(queryparams?.Sort);
        }

        // Get All With Paging
        public static IQueryable<TEntity> Build<TEntity>(this IQueryable<TEntity> query, QueryParams queryparams)
        {
            return query.BuildQuery(queryparams);
        }

        // Get All With FilterQuery[]
        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, FilterQuery[] filterqueries)
        {
            if (filterqueries == null || filterqueries.Length == 0)
            {
                return query;
            }

            foreach (FilterQuery filterquery in filterqueries)
            {
                query = query.BuildQuery(filterquery);
                var a = query.BuildQuery(filterquery).ToList();
            }

            return query;
        }

        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, QueryParams queryparams)
        {
            return query.BuildQuery(queryparams?.Filters).BuildQuery(queryparams?.Sort).BuildQuery(queryparams?.Page);
        }

        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, FilterQuery filterQuery)
        {
            ParameterExpression parameterExpression = Expression.Parameter(query.ElementType, "x");
            Expression<Func<TEntity, bool>> lamda = GetLambda<TEntity>(filterQuery, parameterExpression);
            query = query.Where(lamda);

            return query;
        }

        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, SortQuery sortQuery)
        {
            //ParameterExpression parameterExpression = Expression.Parameter(query.ElementType, "x");
            //Expression<Func<TEntity, bool>> lamda = GetLambda<TEntity>(filterQuery, parameterExpression);
            //query = query.Where(lamda);

            return query;
        }

        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, PageQuery pageQuery)
        {
            int num = pageQuery.PageNumber - 1;
            return query.Skip(num * pageQuery.PageSize).Take(pageQuery.PageSize);
        }

        public static Expression<Func<TEntity, bool>> GetLambda<TEntity>(FilterQuery filterQuery, ParameterExpression parameterExpression)
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperties().Single((PropertyInfo x) => x.Name.Equals(filterQuery.Name, StringComparison.InvariantCultureIgnoreCase));

            MemberExpression property = Expression.Property(parameterExpression, propertyInfo);

            var value = Expression.Constant(Convert.ChangeType(filterQuery.Value, propertyInfo.PropertyType));

            var equals = Expression.Equal(property, value);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameterExpression);
            return lambda;

            //Expression<Func<TEntity, bool>> expressionTree = Expression
        }
    }
}
