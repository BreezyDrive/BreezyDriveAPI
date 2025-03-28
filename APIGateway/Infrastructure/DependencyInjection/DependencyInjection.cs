using Microsoft.OpenApi.Models;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Swagger;
using Yarp.ReverseProxy.Swagger.Extensions;

namespace APIGateway.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy(configuration);
        services.AddSwaggerDocumentation();  
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
    
    
    private static RouteConfig[] GetRoutes()
    {
        return new[]
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
            
            //Cars-route
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
            },
            //authentication-route
            new RouteConfig
            {
                RouteId = "authentication-route",
                ClusterId = "authentication-cluster",
                Match = new RouteMatch { Path = "authentication-api/{**catch-all}" },
                Transforms = new[]
                {
                    new Dictionary<string, string> { ["PathPattern"] = "{**catch-all}" }
                }
            }
        };
    }

    private static ClusterConfig[] GetClusters()
    {
        return new[]
        {
           new ClusterConfig
            {
                ClusterId = "users-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8180" } }
                }, 

            },
            new ClusterConfig
            {
                ClusterId = "cars-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8280" } }
                },

            },
            new ClusterConfig
            {
                ClusterId = "conversations-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8380" } }
                },

            },
            
            new ClusterConfig
            {
                ClusterId = "authentication-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8480" } }
                },

            },
        };
    }

    
    private static ReverseProxyDocumentFilterConfig GetSwaggerConfig()
{
    return new ReverseProxyDocumentFilterConfig
    {
        Routes = GetRoutes().ToDictionary(_ => _.RouteId, _ => _),
        Clusters = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster>
        {
            {
                "users-cluster", new ReverseProxyDocumentFilterConfig.Cluster
                {
                    Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                    {
                        {
                            "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
                            {
                                Address = "http://localhost:8180",
                                Swaggers = new[]
                                {
                                    new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
                                    {
                                        PrefixPath = "/users-api",
                                        Paths = new[] { "/swagger/v1/swagger.json" }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            {
                "cars-cluster", new ReverseProxyDocumentFilterConfig.Cluster
                {
                    Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                    {
                        {
                            "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
                            {
                                Address = "http://localhost:8280",
                                Swaggers = new[]
                                {
                                    new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
                                    {
                                        PrefixPath = "/cars-api",
                                        Paths = new[] { "/swagger/v1/swagger.json" }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            {
                "conversations-cluster", new ReverseProxyDocumentFilterConfig.Cluster
                {
                    Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                    {
                        {
                            "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
                            {
                                Address = "http://localhost:8380",
                                Swaggers = new[]
                                {
                                    new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
                                    {
                                        PrefixPath = "/conversations-api",
                                        Paths = new[] { "/swagger/v1/swagger.json" }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            {
                "authentication-cluster", new ReverseProxyDocumentFilterConfig.Cluster
                {
                    Destinations = new Dictionary<string, ReverseProxyDocumentFilterConfig.Cluster.Destination>
                    {
                        {
                            "destination1", new ReverseProxyDocumentFilterConfig.Cluster.Destination
                            {
                                Address = "http://localhost:8480",
                                Swaggers = new[]
                                {
                                    new ReverseProxyDocumentFilterConfig.Cluster.Destination.Swagger
                                    {
                                        PrefixPath = "/authentication-api",
                                        Paths = new[] { "/swagger/v1/swagger.json" }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    };
}

    
    
    

    
}