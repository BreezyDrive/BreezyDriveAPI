using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Application.Services;
using BreezyDrive.UserServices.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.UserServices.Application.Messaging;
using MassTransit;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;

namespace BreezyDrive.UserServices.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices();         // CORS, Controllers, HttpContext
            services.AddInfrastructure(configuration);  // Database, Repository, External APIs
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

        

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BreezyDrive.UserServices_API", Version = "v1" });

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

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("UserDB");
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                });
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<UserDbContext>>();

        }

        /// Đăng ký Interface với Service
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IUserDriveLisenceService, UserDriveLisenceService>();
        }
        
        private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IMessageHandler<CheckUserExistRequest, CheckUserExistResponse>, CheckUserExistHandler>();
            //thêm dòng này cho từng event
            services.AddGenericConsumer<CheckUserExistRequest, CheckUserExistResponse, CheckUserExistHandler>();
            services.AddGenericConsumer<CheckGoogleExistRequestEvent, CheckGoogleExistResponseEvent, CheckGoogleEmailHandler>();

            
            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();
                
                //configure.AddConsumer(typeof(GenericConsumer<CheckUserExistRequest, CheckUserExistResponse>));
                //thêm consumer vào đây
                configure.AddConsumer<GenericConsumer<CheckUserExistRequest, CheckUserExistResponse>>();

                
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

                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;

        }
        
        private static IServiceCollection AddGenericConsumer<TMessage, TResponse, THandler>(this IServiceCollection services)
            where TMessage : class
            where TResponse : class
            where THandler : class, IMessageHandler<TMessage, TResponse>
        {
            // Đăng ký handler cho message
            services.AddScoped<IMessageHandler<TMessage, TResponse>, THandler>();

            // Đăng ký GenericConsumer
            services.AddScoped<IConsumer<TMessage>, GenericConsumer<TMessage, TResponse>>();

            return services;
        }
    }
}
