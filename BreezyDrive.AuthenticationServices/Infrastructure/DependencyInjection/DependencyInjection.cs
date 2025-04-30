using BreezyDrive.AuthenticationServices.Application.Services;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Utils;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.AuthenticationServices.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Swashbuckle.AspNetCore.Filters;
using AuthenticationService = BreezyDrive.AuthenticationServices.Application.Services.AuthenticationService;
using IAuthenticationService = BreezyDrive.AuthenticationServices.Application.Interfaces.IAuthenticationService;
using BreezyDrive.AuthenticationServices.Application.Messaging;
using BreezyDrive.CommonService.Infrastuctures.Extensions;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using BreezyDrive.CommonService.Infrastuctures.Messaging;

namespace BreezyDrive.AuthenticationServices.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices();
            services.AddAuthenticationServices(configuration);
            services.AddServices();
            services.AddSwaggerDocumentation();
            services.AddMasstransit(configuration);

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

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BreezyDrive.AuthenticationServices_API", Version = "v1" });

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

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenticationAuthorized(configuration);

            services.AddScoped<IAuthentication, Authen>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHashing, Hash>();

            return services;
        }

        private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGenericConsumer<GetUserIdRequestEvent, GetUserIdResponseEvent, GetUserIdHandler>();


            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

                configure.AddConsumer<GenericConsumer<GetUserIdRequestEvent, GetUserIdResponseEvent>>();

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

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
