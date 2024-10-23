using System.Linq.Expressions;
using System.Reflection;

namespace StorageSystem.Order.Application.Extensions
{
    public static class EntityDiffer<T>
    {
        private static readonly Func<T, T, Dictionary<string, (object Value1, object Value2)>> _comparer;

        static EntityDiffer()
        {
            _comparer = GenerateComparer();
        }

        public static Dictionary<string, (object Value1, object Value2)> GetDifferences(T obj1, T obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                throw new ArgumentNullException("Neither of the objects can be null");
            }

            return _comparer(obj1, obj2);
        }

        private static Func<T, T, Dictionary<string, (object Value1, object Value2)>> GenerateComparer()
        {
            Type typeFromHandle = typeof(T);
            ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "x");
            ParameterExpression parameterExpression2 = Expression.Parameter(typeFromHandle, "y");
            ParameterExpression parameterExpression3 = Expression.Variable(typeof(Dictionary<string, (object, object)>), "differences");
            ConstructorInfo constructor = typeof(Dictionary<string, (object, object)>).GetConstructor(Type.EmptyTypes);
            MethodInfo method = typeof(Dictionary<string, (object, object)>).GetMethod("Add");
            List<Expression> list = new List<Expression> { Expression.Assign(parameterExpression3, Expression.New(constructor)) };
            foreach (PropertyInfo item2 in from p in typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                           where p.CanRead && !IsClassType(p.PropertyType)
                                           select p)
            {
                MemberExpression memberExpression = Expression.Property(parameterExpression, item2);
                MemberExpression memberExpression2 = Expression.Property(parameterExpression2, item2);
                Expression test = ((!IsValueType(item2.PropertyType)) ? Expression.NotEqual(Expression.Convert(memberExpression, typeof(object)), Expression.Convert(memberExpression2, typeof(object))) : Expression.NotEqual(memberExpression, memberExpression2));
                ConditionalExpression item = Expression.IfThen(test, Expression.Call(parameterExpression3, method, Expression.Constant(item2.Name), Expression.New(typeof((object, object)).GetConstructor(new Type[2]
                {
                typeof(object),
                typeof(object)
                }), Expression.Convert(memberExpression, typeof(object)), Expression.Convert(memberExpression2, typeof(object)))));
                list.Add(item);
            }

            list.Add(parameterExpression3);
            return Expression.Lambda<Func<T, T, Dictionary<string, (object, object)>>>(Expression.Block(new ParameterExpression[1] { parameterExpression3 }, list), new ParameterExpression[2] { parameterExpression, parameterExpression2 }).Compile();
        }

        private static bool IsClassType(Type type)
        {
            if (type.IsClass)
            {
                return type != typeof(string);
            }

            return false;
        }

        private static bool IsValueType(Type type)
        {
            return type.IsValueType;
        }
    }
}
