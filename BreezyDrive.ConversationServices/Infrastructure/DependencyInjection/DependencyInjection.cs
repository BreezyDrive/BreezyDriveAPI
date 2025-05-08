using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Application.Messaging;
using BreezyDrive.ConversationServices.Application.Services;
using Library.EventContracts.Events.NotificationEvents.Request;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using MongoDB.Driver;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;

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
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true);
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
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB Database for Conversation service
            services.AddSingleton<IMongoDatabase>(provider =>
            {
                var mongoSettings = configuration.GetSection("MongoDB:Conversation");
                var connectionString = mongoSettings["ConnectionString"];
                var databaseName = mongoSettings["DatabaseName"];

                var mongoClient = new MongoClient(connectionString);
                return mongoClient.GetDatabase(databaseName);
            });

            // Register ConversationDbContext
            services.AddScoped<ConversationDbContext>();

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
        }

        private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageHandler<CheckUserExistRequest, CheckUserExistResponse>, CheckUserExistHandler>();
            
            services.AddMassTransit(config =>
            {
                config.AddConsumer<GenericConsumer<NotificationEvent, NotificationResponseEvent>>();
                config.AddConsumer<GenericConsumer<CheckUserExistRequest, CheckUserExistResponse>>();

                config.UsingRabbitMq((context, cfg) =>
                {
                    var configuration = context.GetRequiredService<IConfiguration>();
                    var rabbitMQSettings = configuration.GetSection("RabbitMQ");

                    cfg.Host(rabbitMQSettings["Host"], h =>
                    {
                        h.Username(rabbitMQSettings["Username"]);
                        h.Password(rabbitMQSettings["Password"]);
                    });

                    // Configure request client
                    //cfg.RequestClient<CheckUserExistRequest>(new Uri("queue:user-check-queue"));

                    cfg.ReceiveEndpoint("notification-queue", e =>
                    {
                        e.ConfigureConsumer<GenericConsumer<NotificationEvent, NotificationResponseEvent>>(context);
                    });

                    cfg.ReceiveEndpoint("user-check-queue", e =>
                    {
                        e.ConfigureConsumer<GenericConsumer<CheckUserExistRequest, CheckUserExistResponse>>(context);
                    });
                });
            });

            return services;
        }
    }
}
