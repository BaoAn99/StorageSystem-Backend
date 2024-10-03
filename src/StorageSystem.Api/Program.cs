using StorageSystem.Application;
using StorageSystem.Infrastructure;
using StorageSystem.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
var services = builder.Services;

services.AddSqlDbContext<ApplicationDbContext>(connection)
    .AddDbContextFactory<ApplicationDbContext>();

// Add service application
services.AddApplicationServiceRegistration();
// Add service infrastructure
services.AddRepositoryServiceRegistration();

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
