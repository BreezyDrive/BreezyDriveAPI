using BreezyDrive.Cars.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BreezyDrive.Cars.Infrastructure.DependencyInjection;

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
        services.AddDbContext<CarDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("CarDB");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
            });
        });
    }
}