using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.NotificationServices.Application.DTOs.Request;

using BreezyDrive.NotificationServices.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNotificationServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseApplicationMiddleware();
app.MapControllers();
app.Run();
