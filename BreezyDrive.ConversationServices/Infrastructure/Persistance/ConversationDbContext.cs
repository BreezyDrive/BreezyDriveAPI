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


        //private void EnsureCollectionsExist()
        //{
        //    var existingCollections = _mongoDatabase.ListCollectionNames().ToList();

        //    if (!existingCollections.Contains("Conversations"))
        //        _mongoDatabase.CreateCollection("Conversations");

        //    if (!existingCollections.Contains("ConversationMessages"))
        //        _mongoDatabase.CreateCollection("ConversationMessages");

        //    if (!existingCollections.Contains("MessageFiles"))
        //        _mongoDatabase.CreateCollection("MessageFiles");
        //}


        public ConversationDbContext(IConfiguration configuration)
        {
            Console.WriteLine("🔄 ConversationDBContext is initializing...");

            if (configuration == null)
            {
                Console.WriteLine("❌ Configuration is NULL!");
                throw new ArgumentNullException(nameof(configuration));
            }

            var mongoSettings = configuration.GetSection("MongoDB:Conversation");
            var connectionString = mongoSettings["ConnectionString"];
            var databaseName = mongoSettings["DatabaseName"];

            Console.WriteLine($"🔍 MongoDB Connection String: {connectionString}");
            Console.WriteLine($"📂 Database Name: {databaseName}");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("❌ MongoDB connection string is empty!");
                throw new InvalidOperationException("MongoDB connection string is missing in appsettings.json.");
            }

            var mongoClient = new MongoClient(connectionString);
            _mongoDatabase = mongoClient.GetDatabase(databaseName);

            // Create collections if they don't exist
            var existingCollections = _mongoDatabase.ListCollectionNames().ToList();
            
            if (!existingCollections.Contains("Conversations"))
                _mongoDatabase.CreateCollection("Conversations");
            
            if (!existingCollections.Contains("ConversationMessages"))
                _mongoDatabase.CreateCollection("ConversationMessages");
            
            if (!existingCollections.Contains("MessageFiles"))
                _mongoDatabase.CreateCollection("MessageFiles");

            _conversationColletion = _mongoDatabase.GetCollection<Conversation>("Conversations");
            _conversationMessageColletion = _mongoDatabase.GetCollection<ConversationMessage>("ConversationMessages");
            _messageColletion = _mongoDatabase.GetCollection<MessageFile>("MessageFiles");
        }

        public IMongoCollection<Conversation> Conversations => _conversationColletion;

        public IMongoCollection<ConversationMessage> ConversationMessages => _conversationMessageColletion;

        public IMongoCollection<MessageFile> MessageFiles => _messageColletion;
    }
    
}
