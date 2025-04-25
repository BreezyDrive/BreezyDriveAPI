using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Application.Messaging;
using BreezyDrive.ConversationServices.Application.Services;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;
using BreezyDrive.ConversationServices.Infrastructure.Repositories;
using BreezyDrive.NotificationServices.Application.Interfaces;
using BreezyDrive.NotificationServices.Application.Messaging;
using BreezyDrive.NotificationServices.Application.Services;
using BreezyDrive.NotificationServices.Infrastructure.Persistance;
using BreezyDrive.NotificationServices.Infrastructure.Repositories;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Application.Services;
using BreezyDrive.UserServices.Infrastructure.Persistance;
using Library.EventContracts.Events.NotificationEvents.Request;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BreezyDrive.ConversationServices.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices();         // CORS, Controllers, HttpContext
            services.AddInfrastructure(configuration);  // Database, Repository, External APIs
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
                        /*.AllowCredentials()*/
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

                /*option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });*/
                option.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            // MongoDB UnitOfWork for Conversation service
            services.AddScoped<IMongoUnitiOfWork, MongoUnitOfWork1>();

            // MongoDB UnitOfWork for Notification service
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
            
            // MySQL UnitOfWork for User service
            services.AddScoped<IUnitOfWork, UnitOfWork<UserDbContext>>();
        }

        /// Đăng ký Interface với Service
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IConversationMessageService, ConversationMessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB Database for Conversation service
            services.AddSingleton<ConversationDbContext>(provider =>
                new ConversationDbContext(provider.GetRequiredService<IConfiguration>()));

            // MongoDB Database for Notification service
            services.AddSingleton<NotificationDBContext>(provider =>
                new NotificationDBContext(provider.GetRequiredService<IConfiguration>()));

            // MySQL Database for User service
            services.AddDbContext<UserDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("UserDB");
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                });
            });
        }

        private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageHandler<NotificationEvent, NotificationResponseEvent>, NotificationHandler>();
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

                    cfg.ReceiveEndpoint("notification-queue", e =>
                    {
                        e.ConfigureConsumer<GenericConsumer<NotificationEvent, NotificationResponseEvent>>(context);
                    });
                });
            });
            return services;
        }
    }
}
