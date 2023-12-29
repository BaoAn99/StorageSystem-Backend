using Microsoft.Extensions.DependencyInjection;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Contracts.Features.Auths;
using StorageSystem.Application.Features.Auths;
using StorageSystem.Application.Features.Services;
using StorageSystem.Application.MapperProfiles;
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
            //services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Program));
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IProductImageService, ProductImageService>();
            //services.AddTransient<Irepository<Product>, ProductRepository>();

            return services;
        }
    }
}
