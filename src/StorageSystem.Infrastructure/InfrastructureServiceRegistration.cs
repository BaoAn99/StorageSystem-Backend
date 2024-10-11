using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Domain.Entities.Warehouses;
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
            services.AddScoped(typeof(IRepositoryBaseAsync<,>), typeof(RepositoryBaseAsync<,>));
            services.AddScoped(typeof(IProductRepository<,>), typeof(ProductRepository<,>));
            services.AddScoped(typeof(ISupplierRepository<,>), typeof(SupplierRepository<,>));
            services.AddScoped(typeof(IProductTypeRepository<,>), typeof(ProductTypeRepository<,>));
            services.AddScoped(typeof(IConversionSpecProductRepository<,>), typeof(ConversionSpecProductRepository<,>));
            services.AddScoped(typeof(IProductUnitRepository<,>), typeof(ProductUnitRepository<,>));
            services.AddScoped(typeof(IInvoiceRepository<,>), typeof(InvoiceRepository<,>));
            services.AddScoped(typeof(IWarehouseRepository<,>), typeof(WarehouseRepository<,>));
            services.AddScoped(typeof(IWarehouseInboundRepository<,>), typeof(WarehouseInboundRepository<,>));

            return services;
        }
    }
}
