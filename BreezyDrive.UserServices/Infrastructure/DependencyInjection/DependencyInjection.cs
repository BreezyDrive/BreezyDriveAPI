using BreezyDrive.UserServices.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BreezyDrive.UserServices.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            /*services.AddRepositories();

            services.AddService();

            services.AddAuthen(configuration);

            services.AddAutoMapper(typeof(AutoMapperProfile));*/

            return services;
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("UserDB");
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                });
            });
        }

    }
}
