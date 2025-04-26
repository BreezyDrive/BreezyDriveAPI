using BreezyDrive.CarServices;
using BreezyDrive.CarServices.Infrastructure.DependencyInjection;
using BreezyDrive.CarServices.Infrastructure.Persistence;
using BreezyDrive.CarServices.Infrastructure.Plugins;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Configuration
    .AddJsonFile(Path.GetFullPath(Path.Combine(@"../BreezyDrive.CommonService/shared.appsettings.json")), optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApplicationMiddleware();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
