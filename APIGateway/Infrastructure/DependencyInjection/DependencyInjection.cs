using APIGateway.Infrastructure.Helper;
using Microsoft.OpenApi.Models;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Swagger;
using Yarp.ReverseProxy.Swagger.Extensions;

namespace APIGateway.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiGatewayServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddReverseProxy(configuration);
        services.AddSwaggerDocumentation();
        services.AddCoreServices();
        return services;
    }

    private static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        //services.AddControllers();
        //services.AddHttpContextAccessor();

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
        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddReverseProxy(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddReverseProxy()
        //     .LoadFromMemory(GetRoutes(), GetClusters()).AddSwagger(GetSwaggerConfig());
        
        services.AddReverseProxy()
            .LoadFromMemory(ClusterConfigurationHelper.GetRoutes(), ClusterConfigurationHelper.GetClusters(configuration))
            .AddSwagger(ClusterConfigurationHelper.GetSwaggerConfig(configuration));


        return services;
    }
    
}