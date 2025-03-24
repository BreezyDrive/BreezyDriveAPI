using BreezyDrive.NotificationServices.Domain.Entities;
using BreezyDrive.NotificationServices.Infrastructure.Persistance;
using MongoDB.Driver;

namespace BreezyDrive.NotificationServices.Infrastructure.Repositories
{
    public class MongoUnitOfWork :IMongoUnitOfWork
    {
        private readonly NotificationDBContext _dbContext;

        public MongoUnitOfWork(NotificationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Truyền ra collection Notification
        public IMongoCollection<Notification> Notifications => _dbContext.Notifications;
    }
}
