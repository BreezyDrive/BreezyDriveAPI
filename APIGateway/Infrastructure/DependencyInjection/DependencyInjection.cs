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
        services.AddReverseProxy()
            .LoadFromMemory(GetRoutes(), GetClusters()).AddSwagger(GetSwaggerConfig());


        return services;
    }


    // private static RouteConfig[] GetRoutes()
    // {
    //     return
    //     [
    //         //users-route
    //         new RouteConfig
    //         {
    //             RouteId = "users-route",
    //             ClusterId = "users-cluster",
    //             Match = new RouteMatch { Path = "users-api/{**catch-all}" },
    //             Transforms =
    //             [
    //                 new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
    //             ]
    //         },
    //
    //         //Cars-route
    //         new RouteConfig
    //         {
    //             RouteId = "cars-route",
    //             ClusterId = "cars-cluster",
    //             Match = new RouteMatch { Path = "cars-api/{**catch-all}" },
    //             Transforms =
    //             [
    //                 new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
    //             ]
    //         },
    //
    //         //conversations-route
    //         new RouteConfig
    //         {
    //             RouteId = "conversations-route",
    //             ClusterId = "conversations-cluster",
    //             Match = new RouteMatch { Path = "conversations-api/{**catch-all}" },
    //             Transforms =
    //             [
    //                 new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
    //             ]
    //         },
    //         //authentication-route
    //         new RouteConfig
    //         {
    //             RouteId = "authentication-route",
    //             ClusterId = "authentication-cluster",
    //             Match = new RouteMatch { Path = "authentication-api/{**catch-all}" },
    //             Transforms =
    //             [
    //                 new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
    //             ]
    //         }
    //     ];
    // }
    //
    // private static ClusterConfig[] GetClusters()
    // {
    //     return
    //     [
    //         new ClusterConfig
    //         {
    //             ClusterId = "users-cluster",
    //             Destinations = new Dictionary<string, DestinationConfig>
    //             {
    //                 { "destination1", new DestinationConfig { Address = "http://localhost:8180" } }
    //             },
    //         },
    //         new ClusterConfig
    //         {
    //             ClusterId = "cars-cluster",
    //             Destinations = new Dictionary<string, DestinationConfig>
    //             {
    //                 { "destination1", new DestinationConfig { Address = "http://localhost:8280" } }
    //             },
    //         },
    //         new ClusterConfig
    //         {
    //             ClusterId = "conversations-cluster",
    //             Destinations = new Dictionary<string, DestinationConfig>
    //             {
    //                 { "destination1", new DestinationConfig { Address = "http://localhost:8380" } }
    //             },
    //         },
    //
    //         new ClusterConfig
    //         {
    //             ClusterId = "authentication-cluster",
    //             Destinations = new Dictionary<string, DestinationConfig>
    //             {
    //                 { "destination1", new DestinationConfig { Address = "http://localhost:8480" } }
    //             },
    //         }
    //     ];
    // }
    
    private static RouteConfig[] GetRoutes()
    {
        var routesInfo = new List<(string RouteId, string ClusterId, string PrefixPath)>
        {
            ("users-route", "users-cluster", "users-api"),
            ("cars-route", "cars-cluster", "cars-api"),
            ("conversations-route", "conversations-cluster", "conversations-api"),
            ("authentication-route", "authentication-cluster", "authentication-api")
        };

        return routesInfo.Select(route => new RouteConfig
        {
            RouteId = route.RouteId,
            ClusterId = route.ClusterId,
            Match = new RouteMatch { Path = $"{route.PrefixPath}/{{**catch-all}}" },
            Transforms = 
            [
                new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
            ]
        }).ToArray();
    }

    private static ClusterConfig[] GetClusters()
    {
        var clustersInfo = new List<(string ClusterId, string Address)>
        {
            ("users-cluster", "http://localhost:8180"),
            ("cars-cluster", "http://localhost:8280"),
            ("conversations-cluster", "http://localhost:8380"),
            ("authentication-cluster", "http://localhost:8480")
        };

        return clustersInfo.Select(cluster => new ClusterConfig
        {
            ClusterId = cluster.ClusterId,
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "destination1", new DestinationConfig { Address = cluster.Address } }
            }
        }).ToArray();
    }

    
    
    private static ReverseProxyDocumentFilterConfig GetSwaggerConfig()
    {
        var clustersInfo = new List<(string ClusterName, string Address, string PrefixPath)>
        {
            ("users-cluster", "http://localhost:8180", "/users-api"),
            ("cars-cluster", "http://localhost:8280", "/cars-api"),
            ("conversations-cluster", "http://localhost:8380", "/conversations-api"),
            ("authentication-cluster", "http://localhost:8480", "/authentication-api"),
        };

        return new ReverseProxyDocumentFilterConfig
        {
            Routes = GetRoutes().ToDictionary(c => c.RouteId, c => c),
            Clusters = BuildClusters(clustersInfo)
        };
    }

    private static Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster> BuildClusters(
        List<(string ClusterName, string Address, string PrefixPath)> clustersInfo)
    {
        return clustersInfo.ToDictionary(
            cluster => cluster.ClusterName,
            cluster => new ReverseProxyDocumentFilterConfig.Cluster
            {
                Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                {
                    {
                        "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
                        {
                            Address = cluster.Address,
                            Swaggers = 
                            [
                                new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
                                {
                                    PrefixPath = cluster.PrefixPath,
                                    Paths = ["/swagger/v1/swagger.json"]
                                }
                            ]
                        }
                    }
                }
            });
    }
}