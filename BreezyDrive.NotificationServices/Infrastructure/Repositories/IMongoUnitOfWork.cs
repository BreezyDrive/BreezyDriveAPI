using BreezyDrive.NotificationServices.Domain.Entities;
using MongoDB.Driver;

namespace BreezyDrive.NotificationServices.Infrastructure.Repositories
{
    public interface IMongoUnitOfWork
    {
        IMongoCollection<Notification> Notifications { get; }
    }
}
