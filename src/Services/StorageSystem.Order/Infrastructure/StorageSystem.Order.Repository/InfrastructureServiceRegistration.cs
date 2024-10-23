using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Order.Application.Contracts.Repositories;
using StorageSystem.Order.Application.Contracts.Repositories.Base;
using StorageSystem.Order.Repository.Repositories;
using StorageSystem.Order.Repository.Repositories.Base;

namespace StorageSystem.Order.Repository
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
            services.AddScoped(typeof(ICustomerRepository<,>), typeof(CustomerRepository<,>));

            return services;
        }
    }
}
