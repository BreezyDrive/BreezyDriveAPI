using BreezyDrive.NotificationServices.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BreezyDrive.NotificationServices.Infrastructure.Persistance
{
    public class NotificationDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public NotificationDBContext(IConfiguration configuration)
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

            _database = mongoClient.GetDatabase(databaseName);

            var collectionName = configuration["DatabaseSettings:NotificationsCollection"];
            Console.WriteLine($"📜 Collection Name: {collectionName}");

            _notificationsCollection = _database.GetCollection<Notification>(collectionName);

            //SeedDatabase();
        }



        public IMongoCollection<Notification> Notifications => _notificationsCollection;

        /*private void SeedDatabase()
        {
            Console.WriteLine("⚡ Seeding database...");
            if (_notificationsCollection.CountDocuments(_ => true) == 0)
            {
                var sampleNotification = new Notification
                {
                    ReceiverId = Guid.NewGuid(),
                    Description = "Welcome to MongoDB!",
                    NotificationType = Domain.Enums.NotificationType.Message,
                    IsSeen = false,
                };
                _notificationsCollection.InsertOne(sampleNotification);
                Console.WriteLine("✅ Data inserted!");
            }
            else
            {
                Console.WriteLine("ℹ️ Collection already has data.");
            }
        }*/

    }

}
