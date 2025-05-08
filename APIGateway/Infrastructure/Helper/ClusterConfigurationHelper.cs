using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Swagger;

namespace APIGateway.Infrastructure.Helper;

public static class ClusterConfigurationHelper
{
    private static readonly List<(string ServiceName, string ConfigKey, bool IsRequireAuthentication)> ClusterConfigs =
    [
        ("users", "Users", false),
        ("cars", "Cars", false),
        ("conversations", "Conversations", false),
        ("authentications", "Authentications", false),
        ("bookings", "Bookings", false),
        ("notifications", "Notifications", false),
    ];

    public static RouteConfig[] GetRoutes()
    {
        return ClusterConfigs.Select(service => new RouteConfig
        {
            RouteId = $"{service.ServiceName}-route",
            ClusterId = $"{service.ServiceName}-cluster",
            Match = new RouteMatch { Path = $"{service.ServiceName}-api/{{**catch-all}}" },
            AuthorizationPolicy = service.IsRequireAuthentication ? "DefaultPolicy" : null,
            Transforms =
            [
                new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
            ]
        }).ToArray();
    }

    public static ClusterConfig[] GetClusters(IConfiguration configuration)
    {
        return ClusterConfigs.Select(service =>
        {
            var serviceUrl = configuration[$"ServiceUrls:{service.ConfigKey}"];
            return new ClusterConfig
            {
                ClusterId = $"{service.ServiceName}-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = serviceUrl } }
                }
            };
        }).ToArray();
    }

    public static ReverseProxyDocumentFilterConfig GetSwaggerConfig(IConfiguration configuration)
    {
        return new ReverseProxyDocumentFilterConfig
        {
            Routes = GetRoutes().ToDictionary(c => c.RouteId, c => c),
            Clusters = ClusterConfigs.ToDictionary(
                service => $"{service.ServiceName}-cluster",
                service =>
                {
                    var serviceUrl = configuration[$"ServiceUrls:{service.ConfigKey}"];
                    return new ReverseProxyDocumentFilterConfig.Cluster
                    {
                        Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                        {
                            {
                                "destination1",
                                new ReverseProxyDocumentFilterConfig.Cluster.Destination
                                {
                                    Address = serviceUrl,
                                    Swaggers =
                                    [
                                        new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
                                        {
                                            PrefixPath = $"/{service.ServiceName}-api",
                                            Paths = ["/swagger/v1/swagger.json"]
                                        }
                                    ]
                                }
                            }
                        }
                    };
                })
        };
    }
}
