using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Features.Services;
using StorageSystem.Domain.Commons;
using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ISessionStore, SessionStore>();
            services.AddScoped(typeof(IEntityManager<>), typeof(EntityManager<>));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductUnitService, ProductUnitService>();
            services.AddScoped<IConversionSpecProductService, ConversionSpecProductService>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}
