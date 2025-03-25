using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BreezyDrive.CommonService.Infrastuctures.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection CommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenticationServices(configuration);
            services.AddService();
            return services;
        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHashing, Hash>();

            return services;
        }

        private static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IFirebaseConfiguration, FirebaseConfiguration>();

            return services;
        }
    }
}
