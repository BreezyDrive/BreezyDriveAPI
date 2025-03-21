using Yarp.ReverseProxy.Configuration;

namespace APIGateway.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy(configuration);

        return services;
    }

    private static IServiceCollection AddReverseProxy(this IServiceCollection services, IConfiguration configuration)
    {
        var routes = new List<RouteConfig>
        {
            //users-route
            new RouteConfig
            {
                RouteId = "users-route",
                ClusterId = "users-cluster",
                Match = new RouteMatch { Path = "users-api/{**catch-all}" },
                Transforms = new[]
                {
                    new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
                }
            },
            
            //ars-route
            new RouteConfig
            {
                RouteId = "cars-route",
                ClusterId = "cars-cluster",
                Match = new RouteMatch { Path = "cars-api/{**catch-all}" },
                Transforms = new[]
                {
                    new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
                }
            },
            
            //conversations-route
            new RouteConfig
            {
                RouteId = "conversations-route",
                ClusterId = "conversations-cluster",
                Match = new RouteMatch { Path = "conversations-api/{**catch-all}" },
                Transforms = new[]
                {
                    new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
                }
            }
        };

        var clusters = new List<ClusterConfig>
        {
            new ClusterConfig
            {
                ClusterId = "users-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8180" } }
                }
            },
            new ClusterConfig
            {
                ClusterId = "cars-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8280" } }
                }
            },
            new ClusterConfig
            {
                ClusterId = "conversations-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8380" } }
                }
            }
        };

        services.AddReverseProxy()
                .LoadFromMemory(routes, clusters);

        return services;
    
    }

    
}