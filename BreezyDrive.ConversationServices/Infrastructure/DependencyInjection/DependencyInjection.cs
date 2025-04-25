using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Application.Messaging;
using BreezyDrive.ConversationServices.Application.Services;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;
using BreezyDrive.NotificationServices.Application.Messaging;
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
            services.AddDatabase(configuration);
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
            //services.AddScoped<IUnitOfWork, UnitOfWork<ConversationDbContext>>();

        }

        /// Đăng ký Interface với Service
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IConversationMessageService, ConversationMessageService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ConversationDbContext>(provider =>
                new ConversationDbContext(provider.GetRequiredService<IConfiguration>()));
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
