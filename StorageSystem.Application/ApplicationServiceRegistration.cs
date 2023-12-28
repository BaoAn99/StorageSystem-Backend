using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Features.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {
            //services.AddAutoMapper(c => c.AddProfile<AutoMapper>(), typeof(Program));
            services.AddScoped<IProductService, ProductService>()
                .AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
