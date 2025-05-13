using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.NotificationServices.Application.DTOs.Request;

using BreezyDrive.NotificationServices.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration
        .AddJsonFile(Path.GetFullPath(Path.Combine(@"../BreezyDrive.CommonService/shared.appsettings.json")), optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}
else
{
    builder.Configuration
        .AddJsonFile(Path.GetFullPath(Path.Combine(@"../BreezyDrive.CommonService/shared.appsettings.Production.json")), optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}

builder.Services.AddNotificationServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseApplicationMiddleware();
app.MapControllers();
app.Run();
