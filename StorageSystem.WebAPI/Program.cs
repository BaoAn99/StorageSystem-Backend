using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StorageSystem.Application;
using StorageSystem.Application.AuthService;
using StorageSystem.Application.ProductAppService;
using StorageSystem.Application.ProductImageAppService;
using StorageSystem.DataAccess.IRepository;
using StorageSystem.DataAccess.ProductImageRepository;
using StorageSystem.DataAccess.ProductRepository;
using StorageSystem.EntityFrameworkCore.EntityFrameworkCore;
using StorageSystem.Models.Catalog.ProductImages;
using StorageSystem.Models.Catalog.Products;
using System.Text;
//using StorageSystem.Persistence;
using StorageSystem.DataAccess;
using StorageSystem.Persistence.Contracts;
using StorageSystem.Persistence;

internal class Program
{
    private static void Main(string[] args)
    {

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials();
            });
        });

        //For Identity
        //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        //.AddEntityFrameworkStores<ApplicationDbContext>()
        //.AddDefaultTokenProviders();

        // Adding Authentication
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddJwtBearer(options =>
        //{
        //    options.SaveToken = true;
        //    options.RequireHttpsMetadata = false;
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretKey"))),
        //        ValidateLifetime = true,
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        ClockSkew = TimeSpan.Zero
        //    };
        //});

        // For Entity Framework
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection1")));

        //For Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        //    // Cấu hình về User.
        //    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        //        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        //    options.User.RequireUniqueEmail = true; // Email là duy nhất

        //    // Cấu hình đăng nhập.
        //    //options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
        //    options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại

        //});

        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection1")));
        //builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services
            .AddPersistenceServiceRegistration(builder.Configuration)
            .AddDataAccessServiceRegistration()
            .AddApplicationServiceRegistration();

        });
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<Irepository<Product>, ProductRepository>();
        builder.Services.AddTransient<IProductAppService, ProductAppsService>();
        builder.Services.AddTransient<Irepository<ProductImage>, ProductImageRepository>();
        builder.Services.AddTransient<IProductImageAppservice, ProductImageAppService>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(MyAllowSpecificOrigins);

        app.MapControllers();


        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}