using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace BreezyDrive.CarServices.Infrastructure.Persistence;

public class CarDbContext : BaseDbContext<CarDbContext>
{
    
    public CarDbContext() : base(new DbContextOptions<CarDbContext>()) { }

    protected CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
    
    public DbSet<Cars> Cars { get; set; }
    public DbSet<CarBrands> CarBrands { get; set; }
    public DbSet<CarFeatures> CarFeatures { get; set; }
    public DbSet<CarModels> CarModels { get; set; }
    public DbSet<CarRatings> CarRatings { get; set; }
    public DbSet<CarRegistrations> CarRegistrations { get; set; }
    public DbSet<CarRules> CarRules { get; set; }
    public DbSet<Features> Features { get; set; }
    public DbSet<Rules> Rules { get; set; }

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
            var connectionString = configuration.GetConnectionString("CarDB");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarDbContext).Assembly);
    }
    
}