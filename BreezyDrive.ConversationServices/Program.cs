using BreezyDrive.ConversationServices.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký các services
builder.Services.InfrastructureService(builder.Configuration);

var app = builder.Build();

// Cấu hình Swagger
app.UseSwaggerDocumentation();

// Cấu hình Middleware
app.UseApplicationMiddleware();
app.MapControllers();
app.Run();