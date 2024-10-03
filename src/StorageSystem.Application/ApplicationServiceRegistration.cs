using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Features.Services;

namespace StorageSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();

            return services;
        }
    }
}
