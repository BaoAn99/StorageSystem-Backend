using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Infrastructure.Persistence.Contracts;
using StorageSystem.Infrastructure.Persistence.Contracts.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace StorageSystem.Infrastructure.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServiceRegistration(this IServiceCollection services,
            IConfiguration configuration)
        {
            // For Entity Framework
            //services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(
            //            configuration.GetConnectionString("DefaultConnection"),
            //            x => x.MigrationsAssembly("StorageSystem.Persistence"))
            //        );

            // Add services to the container.
            //var connection = configuration.GetConnectionString("DefaultConnection");

            //services.AddSqlDbContext<ApplicationDbContext>(connection)
            //    .AddDbContextFactory<ApplicationDbContext>();

            //For Identity
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddSqlDbContext<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(delegate (DbContextOptionsBuilder options)
            {
                options.UseSqlServer(connectionString).UseLazyLoadingProxies();
            });
            return services;
        }

        public static IServiceCollection AddDbContextFactory<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddTransient<IDbContextFactory, DbContextFactory<TDbContext>>();
            return services;
        }

        public static void ApplyGlobalFilters<TInterface>(this EntityTypeBuilder entityBuilder, Expression<Func<TInterface, bool>> expression)
        {
            Type clrType = entityBuilder.Metadata.ClrType;
            if (clrType.GetInterface(typeof(TInterface).Name) != null)
            {
                ParameterExpression parameterExpression = Expression.Parameter(clrType);
                Expression body = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), parameterExpression, expression.Body);
                entityBuilder.HasQueryFilter(Expression.Lambda(body, parameterExpression));
            }
        }
        public static void AddDefaultEntitySetting(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType item in from x in modelBuilder.Model.GetEntityTypes()
                                                where x.ClrType.GetInterfaces().Any((Type i) => i == typeof(IEntity))
                                                select x)
            {
                EntityTypeBuilder entityTypeBuilder = modelBuilder.Entity(item.ClrType);
                //if (GlobalEntityFilter.EnableSoftDelete && item.ClrType.GetInterfaces().Any((Type i) => i == typeof(ISoftDelete)))
                //{
                //    entityTypeBuilder.ApplyGlobalFilters((ISoftDelete e) => !e.IsDeleted);
                //}

                //if (GlobalEntityFilter.EnablePublish && item.ClrType.GetInterfaces().Any((Type i) => i == typeof(IHavePublish)))
                //{
                //    entityTypeBuilder.ApplyGlobalFilters((IHavePublish e) => e.IsPublished);
                //}

                foreach (IMutableProperty property in item.GetProperties())
                {
                    if (property.Name == "Code" || property.Name == "Name")
                    {
                        property.IsNullable = false;
                        if (!property.GetMaxLength().HasValue)
                        {
                            property.SetMaxLength(256);
                        }

                        if (!property.IsIndex())
                        {
                            if (property.Name == "Code")
                            {
                                item.AddIndex(property).IsUnique = true;
                            }
                        }
                        else if (property.Name == "Code" && !property.IsUniqueIndex())
                        {
                            item.AddIndex(property).IsUnique = true;
                        }
                    }
                    else if (property.Name == "CreatedAt")
                    {
                        if (!property.IsIndex())
                        {
                            item.AddIndex(property);
                        }
                    }
                    else if (!(property.Name == "UpdatedAt"))
                    {
                        if ((property.Name != "Id" && property.Name.EndsWith("Id")) || property.Name == "IsPublished" || property.Name == "IsDeleted" || property.Name == "DisplayOrder")
                        {
                            if (!property.IsIndex())
                            {
                                item.AddIndex(property);
                            }
                        }
                        else if (property.Name.EndsWith("Date", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Type type = property.ClrType;
                            if (type.IsGenericType)
                            {
                                type = type.GenericTypeArguments[0];
                            }

                            if (type == typeof(DateTime))
                            {
                                entityTypeBuilder.Property(property.Name).HasColumnType("date");
                            }
                        }
                        else if (property.Name.EndsWith("Date", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Type type = property.ClrType;
                            if (type.IsGenericType)
                            {
                                type = type.GenericTypeArguments[0];
                            }

                            if (type == typeof(DateTimeOffset))
                            {
                                entityTypeBuilder.Property(property.Name).HasColumnType("date");
                            }
                        }
                        else if (property.Name.EndsWith("Longitude") || property.Name.EndsWith("Latitude"))
                        {
                            Type type2 = property.ClrType;
                            if (type2.IsGenericType)
                            {
                                type2 = type2.GenericTypeArguments[0];
                            }

                            if (type2 == typeof(decimal))
                            {
                                entityTypeBuilder.Property(property.Name).HasColumnType("decimal(11,8)");
                            }
                        }
                    }

                    if (property.Name.EndsWith("Id"))
                    {
                        Type type3 = property.ClrType;
                        if (type3.IsGenericType)
                        {
                            type3 = type3.GenericTypeArguments[0];
                        }

                        if (type3 == typeof(string) && !property.GetMaxLength().HasValue)
                        {
                            property.SetMaxLength(50);
                        }
                    }

                    if (!(property.Name == "Id"))
                    {
                        continue;
                    }

                    Type clrType = property.ClrType;

                    if (clrType == typeof(decimal) || clrType == typeof(decimal?))
                    {
                        entityTypeBuilder.Property(property.Name).HasColumnType("decimal(11,4)");
                        entityTypeBuilder.Property(property.Name).HasPrecision(18, 2);
                    }

                    if (!(clrType == typeof(int)) && !(clrType == typeof(long)) && !(clrType == typeof(short)) && !(clrType == typeof(byte)) && !(clrType == typeof(uint)) && !(clrType == typeof(ulong)) && !(clrType == typeof(ushort)) && !(clrType == typeof(sbyte)))
                    {
                        continue;
                    }

                    bool flag = item.ClrType.GetFields().Any((FieldInfo x) => x.IsStatic);
                    if (!flag && item.ClrType.BaseType!.IsAbstract)
                    {
                        flag = item.ClrType.BaseType.GetFields().Any((FieldInfo x) => x.IsStatic);
                    }

                    if (flag)
                    {
                        modelBuilder.Entity(item.ClrType).Property(property.Name).ValueGeneratedNever();
                    }
                }
            }
        }

        public static void InjectEntities<TType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            modelBuilder.InjectEntities(typeof(TType), assemblies);
        }

        public static void InjectEntities(this ModelBuilder modelBuilder, Type type, params Assembly[] assemblies)
        {
            for (int i = 0; i < assemblies.Length; i++)
            {
                IEnumerable<Type> source = from x in assemblies[i].GetTypes()
                                           where x.IsClass && !x.IsAbstract
                                           select x;
                source = ((!type.IsInterface) ? source.Where((Type x) => type.IsAssignableFrom(x)) : (type.IsGenericType ? source.Where((Type x) => x.GetInterfaces().Any((Type a) => a.IsGenericType && a.GetGenericTypeDefinition() == type)) : source.Where((Type x) => x.GetInterface(type.Name) != null)));
                foreach (Type item in source)
                {
                    modelBuilder.Entity(item);
                }
            }
        }
    }
}
