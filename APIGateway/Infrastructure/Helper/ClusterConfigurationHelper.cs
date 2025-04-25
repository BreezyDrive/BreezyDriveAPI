using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Swagger;

namespace APIGateway.Infrastructure.Helper;

public static class ClusterConfigurationHelper
{
    //config YARP cluster, route and swagger
    private static readonly List<(string ServiceName, string Address, bool IsRequireAuthentication)> ClusterConfigs =
    [
        ("users", "http://localhost:8180", false),
        ("cars", "http://localhost:8280", false),
        ("conversations", "http://localhost:8380", false),
        ("authentication", "http://localhost:8480", false)
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

    public static ClusterConfig[] GetClusters()
    {
        return ClusterConfigs.Select(service => new ClusterConfig
        {
            ClusterId = $"{service.ServiceName}-cluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "destination1", new DestinationConfig { Address = service.Address } }
            }
        }).ToArray();
    }

    public static ReverseProxyDocumentFilterConfig GetSwaggerConfig()
    {
        return new ReverseProxyDocumentFilterConfig
        {
            Routes = GetRoutes().ToDictionary(c => c.RouteId, c => c),
            Clusters = ClusterConfigs.ToDictionary(
                service => $"{service.ServiceName}-cluster",
                service => new ReverseProxyDocumentFilterConfig.Cluster
                {
                    Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                    {
                        {
                            "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
                            {
                                Address = service.Address,
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
                }
            )
        };
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
    
    // private static RouteConfig[] GetRoutes()
    // {
    //     var routesInfo = new List<(string RouteId, string ClusterId, string PrefixPath)>
    //     {
    //         ("users-route", "users-cluster", "users-api"),
    //         ("cars-route", "cars-cluster", "cars-api"),
    //         ("conversations-route", "conversations-cluster", "conversations-api"),
    //         ("authentication-route", "authentication-cluster", "authentication-api")
    //     };
    //
    //     return routesInfo.Select(route => new RouteConfig
    //     {
    //         RouteId = route.RouteId,
    //         ClusterId = route.ClusterId,
    //         Match = new RouteMatch { Path = $"{route.PrefixPath}/{{**catch-all}}" },
    //         Transforms = 
    //         [
    //             new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
    //         ]
    //     }).ToArray();
    // }
    //
    // private static ClusterConfig[] GetClusters()
    // {
    //     var clustersInfo = new List<(string ClusterId, string Address)>
    //     {
    //         ("users-cluster", "http://localhost:8180"),
    //         ("cars-cluster", "http://localhost:8280"),
    //         ("conversations-cluster", "http://localhost:8380"),
    //         ("authentication-cluster", "http://localhost:8480")
    //     };
    //
    //     return clustersInfo.Select(cluster => new ClusterConfig
    //     {
    //         ClusterId = cluster.ClusterId,
    //         Destinations = new Dictionary<string, DestinationConfig>
    //         {
    //             { "destination1", new DestinationConfig { Address = cluster.Address } }
    //         }
    //     }).ToArray();
    // }
    //
    //
    //
    // private static ReverseProxyDocumentFilterConfig GetSwaggerConfig()
    // {
    //     var clustersInfo = new List<(string ClusterName, string Address, string PrefixPath)>
    //     {
    //         ("users-cluster", "http://localhost:8180", "/users-api"),
    //         ("cars-cluster", "http://localhost:8280", "/cars-api"),
    //         ("conversations-cluster", "http://localhost:8380", "/conversations-api"),
    //         ("authentication-cluster", "http://localhost:8480", "/authentication-api"),
    //     };
    //
    //     return new ReverseProxyDocumentFilterConfig
    //     {
    //         Routes = GetRoutes().ToDictionary(c => c.RouteId, c => c),
    //         Clusters = BuildClusters(clustersInfo)
    //     };
    // }
    //
    // private static Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster> BuildClusters(
    //     List<(string ClusterName, string Address, string PrefixPath)> clustersInfo)
    // {
    //     return clustersInfo.ToDictionary(
    //         cluster => cluster.ClusterName,
    //         cluster => new ReverseProxyDocumentFilterConfig.Cluster
    //         {
    //             Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
    //             {
    //                 {
    //                     "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
    //                     {
    //                         Address = cluster.Address,
    //                         Swaggers = 
    //                         [
    //                             new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
    //                             {
    //                                 PrefixPath = cluster.PrefixPath,
    //                                 Paths = ["/swagger/v1/swagger.json"]
    //                             }
    //                         ]
    //                     }
    //                 }
    //             }
    //         });
    // }
    
}