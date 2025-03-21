using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Data;
using BreezyDrive.CommonService.Infrastuctures.Repositories;
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

            return services;
        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHashing, Hash>();

            return services;
        }
    }
}
