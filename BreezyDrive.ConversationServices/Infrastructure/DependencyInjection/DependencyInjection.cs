using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Application.Services;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;
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
            services.AddScoped<IUnitOfWork, UnitOfWork<ConversationDbContext>>();

        }

        /// Đăng ký Interface với Service
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IConversationMessageService, ConversationMessageService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConversationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("ConversationDB");
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                });
            });
        }
    }
}
