using BreezyDrive.ConversationServices.Domain.Entities;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;
using MongoDB.Driver;

namespace BreezyDrive.ConversationServices.Infrastructure.Repositories
{
    public class MongoUnitOfWork : IMongoUnitiOfWork
    {

        private readonly ConversationDbContext _dbContext;

        public MongoUnitOfWork (ConversationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMongoCollection<Conversation> Conversations => _dbContext.Conversations;

        public IMongoCollection<ConversationMessage> ConversationMessages => _dbContext.ConversationMessages;

        public IMongoCollection<MessageFile> MessageFiles => _dbContext.MessageFiles;
    }
}
