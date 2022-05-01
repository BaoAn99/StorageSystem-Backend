using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using StorageSystem.Application.SendMailService;
using StorageSystem.EntityFrameworkCore.EntityFrameworkCore;
using StorageSystem.Models.MailViewModel;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection");;

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<AppDbContext>();;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOptions();                                        // Kích hoạt Options
var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
builder.Services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject

builder.Services.AddTransient<IEmailSender, SendMailService>();        // Đăng ký dịch vụ Mail

builder.Services.AddDefaultIdentity<IdentityUser>
    (options =>
    {
        options.Password.RequireDigit = true; // Không bắt phải có số
        options.Password.RequireLowercase = true; // Không bắt phải có chữ thường
        options.Password.RequireNonAlphanumeric = true; // Không bắt ký tự đặc biệt
        options.Password.RequireUppercase = true; // Không bắt buộc chữ in
        options.Password.RequiredLength = 4; // Số ký tự tối thiểu của password
        options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

        // Cấu hình Lockout - khóa user
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
        options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
        options.Lockout.AllowedForNewUsers = true;

        // Cấu hình về User.
        options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;  // Email là duy nhất

        // Cấu hình đăng nhập.
        options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
        options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
        //options.SignIn.RequireConfirmedAccount = true;
    })
.AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
