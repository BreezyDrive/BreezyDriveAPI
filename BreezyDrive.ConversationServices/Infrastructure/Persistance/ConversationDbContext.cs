using BreezyDrive.CommonService.Infrastuctures.Data;
using BreezyDrive.ConversationServices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BreezyDrive.ConversationServices.Infrastructure.Persistance
{
    public class ConversationDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Conversation> _conversationColletion;
        private readonly IMongoCollection<ConversationMessage> _conversationMessageColletion;
        private readonly IMongoCollection<MessageFile> _messageColletion;


        public ConversationDbContext (IConfiguration configuration)
        {
            Console.WriteLine("🔄 NotificationDBContext is initializing...");

            if (configuration == null)
            {
                Console.WriteLine("❌ Configuration is NULL!");
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionString = configuration.GetConnectionString("MongoDB");
            Console.WriteLine($"🔍 MongoDB Connection String: {connectionString}");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("❌ MongoDB connection string is empty!");
                throw new InvalidOperationException("MongoDB connection string is missing in appsettings.json.");
            }

            var mongoClient = new MongoClient(connectionString);
            var databaseName = configuration["DatabaseSettings:DatabaseName"];
            Console.WriteLine($"📂 Database Name: {databaseName}");

            _mongoDatabase = mongoClient.GetDatabase(databaseName);

            var collectionName = configuration["DatabaseSettings:ConversationCollection"];
            Console.WriteLine($"📜 Collection Name: {collectionName}");

            _conversationColletion = _mongoDatabase.GetCollection<Conversation>(collectionName);
            _conversationMessageColletion = _mongoDatabase.GetCollection<ConversationMessage>(collectionName);
            _messageColletion = _mongoDatabase.GetCollection<MessageFile>(collectionName);

            //SeedDatabase();
        }

        public IMongoCollection<Conversation> Conversations => _conversationColletion;

        public IMongoCollection<ConversationMessage> ConversationMessages => _conversationMessageColletion;

        public IMongoCollection<MessageFile> MessageFiles => _messageColletion;
    }
    
}
