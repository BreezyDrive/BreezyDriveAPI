using BreezyDrive.AuthenticationServices.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile(Path.GetFullPath(Path.Combine(@"../BreezyDrive.CommonService/shared.appsettings.json")), optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


// Đăng ký các services
builder.Services.AuthenticationServices(builder.Configuration);

var app = builder.Build();

// Cấu hình Swagger
app.UseSwaggerDocumentation();

// Cấu hình Middleware
app.UseApplicationMiddleware();
app.MapControllers();
app.Run();
