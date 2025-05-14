using BreezyDrive.CommonService.Infrastuctures.Data;
using BreezyDrive.UserServices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreezyDrive.UserServices.Infrastructure.Persistance
{
    public class UserDbContext : BaseDbContext<UserDbContext>
    {
        public UserDbContext() : base(new DbContextOptions<UserDbContext>()) { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<UserDriveLicenses> UserDriveLicenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Lấy môi trường từ biến môi trường ASPNETCORE_ENVIRONMENT
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory());
                

                if (environment == "Development")
                {
                    builder
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile(Path.GetFullPath(Path.Combine(@"../BreezyDrive.CommonService/shared.appsettings.json")), optional: true, reloadOnChange: true);
                }
                else
                {
                    builder
                        .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
                        // .AddJsonFile(Path.GetFullPath(Path.Combine(@"../BreezyDrive.CommonService/shared.appsettings.Production.json")), optional: true, reloadOnChange: true);
                        .AddJsonFile("shared.appsettings.Production.json", optional: true, reloadOnChange: true);
                }
                
                var configuration = builder.Build();
                var connectionString = configuration.GetConnectionString("UserDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        }


    }

}
