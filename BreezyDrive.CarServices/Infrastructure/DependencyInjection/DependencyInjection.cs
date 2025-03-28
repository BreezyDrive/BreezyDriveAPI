using System.Text.Json.Serialization;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Application.Services;
using BreezyDrive.CarServices.Infrastructure.Persistence;
using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BreezyDrive.CarServices.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices();         // CORS, Controllers, HttpContext
            services.AddInfrastructure(configuration);  // Database, Repository, External APIs
            services.AddAuthenticationServices(); // JWT, Identity
            services.AddRepositories();         // UnitOfWork, Repository
            services.AddServices();             // Map Interface với Service
            services.AddSwaggerDocumentation();  // Swagger
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddMasstransit(configuration); //RabbitMQ
            // Đăng ký FluentValidation
            services.AddValidator(configuration);
            return services;
        }

        private static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            });
            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
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
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BreezyDrive.CarService_API", Version = "v1" });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("CarDB");
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                });
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<CarDbContext>>();

        }

        /// Đăng ký Interface với Service
        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRuleService, RuleService>();
            services.AddScoped<ICarBrandService, CarBrandService>();
            services.AddScoped<ICarFeatureService, CarFeatureService>();
            services.AddScoped<ICarModelService, CarModelService>();
            services.AddScoped<ICarRatingService, CarRatingService>();
            services.AddScoped<ICarRegistrationService, CarRegistrationService>();
            services.AddScoped<ICarRuleService, CarRuleService>();
            services.AddScoped<IFeatureService, FeatureService>();
            
        }

        private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

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
        
        private static IServiceCollection AddValidator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
            return services;
        }
    }
}
