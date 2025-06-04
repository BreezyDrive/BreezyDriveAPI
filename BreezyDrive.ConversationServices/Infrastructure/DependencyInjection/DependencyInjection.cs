using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Data;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using BreezyDrive.ConversationServices.Application.Hubs;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Application.Services;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;
using Library.EventContracts.Events.NotificationEvents.Request;
using Library.EventContracts.Events.UserEvents.Request;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Filters;

namespace BreezyDrive.ConversationServices.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices();         // CORS, Controllers, HttpContext
            services.AddInfrastructure(configuration);  // Database, Repository
            services.AddAuthenticationServices(); // JWT, Identity
            services.AddRepositories();         // UnitOfWork, Repository
            services.AddServices();             // Map Interface với Service
            services.AddSwaggerDocumentation();  // Swagger
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddMasstransit(configuration); //RabbitMQ
            return services;
        }

        private static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    // Thay thế AllowAnyOrigin() và SetIsOriginAllowed(_ => true)
                    // bằng WithOrigins() và chỉ định rõ các origin được phép
                    builder
                        .WithOrigins("http://localhost:5173"
                                    , "https://localhost:8081/swagger") // API Gateway của bạn
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); // Rất quan trọng cho SignalR
                });
            });

            return services;
        }

        private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            return services;
        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.EnableAnnotations();
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BreezyDrive.ConversationServices_API", Version = "v1" });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            // Không cần thêm gì ở đây vì đã sử dụng MongoDB repository từ Common package
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IConversationMessageService, ConversationMessageService>();
            services.AddScoped<IMessageFileService, MessageFileService>();
            services.AddScoped<IConversationHubService, ConversationHubService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB Database for Conversation service
            //services.AddSingleton<IMongoDatabase>(provider =>
            //{
            //    var mongoSettings = configuration.GetSection("MongoDB:Conversation");
            //    var connectionString = mongoSettings["ConnectionString"];
            //    var databaseName = mongoSettings["DatabaseName"];

            //    var mongoClient = new MongoClient(connectionString);
            //    return mongoClient.GetDatabase(databaseName);
            //});

            var connectionString = configuration.GetConnectionString("ConversationDB");
            Console.WriteLine($"ConnectionString: {connectionString}");

            var databaseName = configuration["DatabaseSettings:DatabaseName"];

            var database = MongoDbInitializer.Initialize(connectionString, databaseName);

            // Register ConversationDbContext
            services.AddScoped<ConversationDbContext>();
            services.AddSingleton<IMongoDatabase>(database);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
        }

        private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

                // Add consumers
                configure.AddConsumer<GenericConsumer<NotificationEvent, NotificationResponseEvent>>();

                // Add request clients
                configure.AddRequestClient<CheckUserExistRequest>();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqConfig = configuration.GetSection("RabbitMQ");
                    var host = rabbitMqConfig["Host"];
                    var username = rabbitMqConfig["Username"];
                    var password = rabbitMqConfig["Password"];

                    // Kiểm tra xem các giá trị có null hoặc rỗng không
                    if (string.IsNullOrWhiteSpace(host))
                        throw new Exception("RabbitMQ Host is not configured.");
                    if (string.IsNullOrWhiteSpace(username))
                        throw new Exception("RabbitMQ Username is not configured.");
                    if (string.IsNullOrWhiteSpace(password))
                        throw new Exception("RabbitMQ Password is not configured.");

                    cfg.Host(new Uri(host), h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    // Tự động cấu hình endpoints cho tất cả consumers
                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
