using BreezyDrive.ConversationServices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BreezyDrive.ConversationServices.Infrastructure.Persistance
{
    public class ConversationDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Conversation> _conversationColletion;
        private readonly IMongoCollection<ConversationMessage> _conversationMessageColletion;
        private readonly IMongoCollection<MessageFile> _messageColletion;
        
        static ConversationDbContext()
        {
            // Register GUID serializer only once when the class is first used
            if (!BsonSerializer.TryRegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard)))
            {
                // If registration fails, the serializer is already registered
                var currentSerializer = BsonSerializer.LookupSerializer<Guid>();
                if (currentSerializer is GuidSerializer guidSerializer && guidSerializer.GuidRepresentation != GuidRepresentation.Standard)
                {
                    // If the current serializer uses a different representation, we might want to log a warning
                    Console.WriteLine("Warning: Existing Guid serializer uses different representation");
                }
            }
        }

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

            var connectionString = configuration.GetConnectionString("ConversationDB");
            Console.WriteLine($"ConnectionString: {connectionString}");

            var databaseName = configuration["DatabaseSettings:DatabaseName"];

            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            mongoClientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var mongoClient = new MongoClient(mongoClientSettings);
            _mongoDatabase = mongoClient.GetDatabase(databaseName);

            Console.WriteLine($"🔍 MongoDB Connection String: {connectionString}");
            Console.WriteLine($"📂 Database Name: {databaseName}");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("❌ MongoDB connection string is empty!");
                throw new InvalidOperationException("MongoDB connection string is missing in appsettings.json.");
            }

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
