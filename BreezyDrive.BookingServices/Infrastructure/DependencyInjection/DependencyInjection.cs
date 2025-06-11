using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Application.Services;
using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Extensions;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
using BreezyDrive.CommonService.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace BreezyDrive.BookingServices.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCoreServices();
        services.AddCorsService();
        services.AddServices();
        services.AdDatabaseServices(configuration);
        services.AddSwaggerDocumentation();
        services.AddMasstransit(configuration);
        services.AddValidator(configuration);
        services.AddAuthenticationServices(configuration);
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        

        return services;
    }


    private static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        return services;
    }

    private static IServiceCollection AddCorsService(this IServiceCollection services)
    {
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


    private static IServiceCollection AdDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BookingDB");

        var mongoUrl = MongoUrl.Create(connectionString);
        var databaseName = mongoUrl.DatabaseName;

        var mongoClient = new MongoClient(mongoUrl);
        var database = mongoClient.GetDatabase(databaseName);

        services.AddSingleton<IMongoDatabase>(database);

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();


        return services;
    }


    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthenticationAuthorized(configuration);
        return services;
    }


    private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "BreezyDrive.BookingService_API", Version = "v1" });

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

    private static void AddServices(this IServiceCollection service)
    {
        //add scope
        service.AddScoped<IBookingService, BookingService>();
        service.AddScoped<IBookingScheduleService, BookingScheduleService>();
        service.AddScoped<IExistenceCheckerService,  ExistenceCheckerService>();
        service.AddScoped<IBookingPermissionChecker,  BookingPermissionChecker>();
        service.AddScoped<IBookingStatusHandler,  BookingStatusHandler>();
        service.AddScoped<IBookingPreviewService,  BookingPreviewService>();
        service.AddScoped<ITokenService,  TokenService>();
    }

    private static IServiceCollection AddMasstransit(this IServiceCollection services, IConfiguration configuration)
    {
        //thêm dòng này cho từng event
        //services.AddGenericConsumer<CheckCarExistRequestEvent, EventSuccessResponse, CheckCarExistHandler>();

        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();

            //thêm consumer vào đây
            //configure.AddConsumer<GenericConsumer<CheckCarExistRequestEvent, EventSuccessResponse>>();


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

    private static IServiceCollection AddGenericConsumer<TMessage, TResponse, THandler>(
        this IServiceCollection services)
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

    private static IServiceCollection AddValidator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
        return services;
    }
}