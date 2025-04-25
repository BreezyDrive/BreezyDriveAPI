using BreezyDrive.ConversationServices.Domain.Entities;
using MongoDB.Driver;

namespace BreezyDrive.ConversationServices.Infrastructure.Repositories
{
    public interface IMongoUnitiOfWork
    {
        IMongoCollection<Conversation> Conversations { get; }
        IMongoCollection<ConversationMessage> ConversationMessages { get; }
        IMongoCollection<MessageFile> MessageFiles { get; }
    }
}
