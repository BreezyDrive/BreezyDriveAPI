using BreezyDrive.CommonService.Infrastuctures.Data;
using BreezyDrive.ConversationServices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BreezyDrive.ConversationServices.Infrastructure.Persistance
{
    //public class ConversationDbContext : BaseDbContext<ConversationDbContext>
    //{
    //    public ConversationDbContext() : base(new DbContextOptions<ConversationDbContext>()) { }

    //    public ConversationDbContext(DbContextOptions<ConversationDbContext> options) : base(options) { }

    //    public DbSet<Conversation> Conversations { get; set; }
    //    public DbSet<ConversationMessage> ConversationMessages { get; set; }
    //    public DbSet<MessageFile> MessageFiles { get; set; }
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        if (!optionsBuilder.IsConfigured)
    //        {
    //            IConfigurationRoot configuration = new ConfigurationBuilder()
    //               .SetBasePath(Directory.GetCurrentDirectory())
    //               .AddJsonFile("appsettings.json")
    //               .Build();
    //            var connectionString = configuration.GetConnectionString("ConversationDB");

    //            optionsBuilder.UseSqlServer(connectionString);
    //        }
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConversationDbContext).Assembly);
    //    }
    //}

    public class ConversationDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
    }
}
