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
        public DbSet<UserDriveLisences> UserDriveLisences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
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
