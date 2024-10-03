using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Infrastructure.Repositories;
using StorageSystem.Infrastructure.Repositories.Base;

namespace StorageSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddRepositoryServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(ISupplierRepository<,>), typeof(SupplierRepository<,>));
            services.AddScoped(typeof(ISupplierRepository<,>), typeof(SupplierRepository<,>));

            return services;
        }
    }
}
