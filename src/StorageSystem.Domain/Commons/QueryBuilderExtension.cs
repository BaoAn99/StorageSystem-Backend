using StorageSystem.Domain.Commons;
using System.Linq.Expressions;
using System.Reflection;

namespace storagesystem.domain.commons
{
    public static class QueryBuilderExtension
    {
        // Get All With Paging
        public static IQueryable<TEntity> Build<TEntity>(this IQueryable<TEntity> query, QueryParams queryparams)
        {
            return query.BuildQuery(queryparams);
        }
        
        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, QueryParams queryParams)
        {
            return query.BuildQuery(queryParams?.Filters).BuildQuery(queryParams?.Sort).BuildQuery(queryParams?.Page);
        }
        
        // Get All Without Paging
        public static IQueryable<TEntity> BuildQueryWithoutPaging<TEntity>(this IQueryable<TEntity> query, QueryParamsWithoutPaging queryParams)
        {
            return query.BuildQuery(queryParams);
        }
        
        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, QueryParamsWithoutPaging queryParams)
        {
            return query.BuildQuery(queryParams?.Filters).BuildQuery(queryParams?.Sort);
        }

        // Get All With FilterQuery[]
        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, FilterQuery[] filterQueries)
        {
            if (filterQueries == null || filterQueries.Length == 0)
            {
                return query;
            }

            foreach (FilterQuery filterQuery in filterQueries)
            {
                query = query.BuildQuery(filterQuery);
            }

            return query;
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
            if (sortQuery == null || sortQuery.Name == null) return query;
            ParameterExpression parameterExpression = Expression.Parameter(query.ElementType, "x");
            Expression<Func<TEntity, object>> lamda = GetLambda<TEntity>(sortQuery, parameterExpression);

            query = sortQuery.IsAscending ? query.OrderBy(lamda) : query.OrderByDescending(lamda);
            return query;
        }

        private static IQueryable<TEntity> BuildQuery<TEntity>(this IQueryable<TEntity> query, PageQuery pageQuery)
        {
            int num = pageQuery.PageNumber - 1;
            return query.Skip(num * pageQuery.PageSize).Take(pageQuery.PageSize);
        }

        private static Expression<Func<TEntity, bool>> GetLambda<TEntity>(FilterQuery filterQuery, ParameterExpression parameterExpression)
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperties().Single(x => x.Name.Equals(filterQuery.Name, StringComparison.InvariantCultureIgnoreCase));

            MemberExpression property = Expression.Property(parameterExpression, propertyInfo);

            var value = Expression.Constant(Convert.ChangeType(filterQuery.Value, propertyInfo.PropertyType));

            var equals = Expression.Equal(property, value);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameterExpression);
            return lambda;

            //Expression<Func<TEntity, bool>> expressionTree = Expression
        }

        private static Expression<Func<TEntity, object>> GetLambda<TEntity>(SortQuery sortQuery, ParameterExpression parameterExpression)
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperties().Single(x => x.Name.Equals(sortQuery.Name, StringComparison.InvariantCultureIgnoreCase));

            MemberExpression property = Expression.Property(parameterExpression, propertyInfo);

            var converted = Expression.Convert(property, typeof(object));
            var sortExpression = Expression.Lambda<Func<TEntity, object>>(converted, parameterExpression);
            return sortExpression;
        }
    }
}
