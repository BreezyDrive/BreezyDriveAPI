using BreezyDrive.CarServices;
using BreezyDrive.CarServices.Infrastructure.DependencyInjection;
using BreezyDrive.CarServices.Infrastructure.Persistence;
using BreezyDrive.CarServices.Infrastructure.Plugins;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApplicationMiddleware();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
