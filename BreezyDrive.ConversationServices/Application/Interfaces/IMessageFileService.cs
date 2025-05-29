using BreezyDrive.ConversationServices.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BreezyDrive.ConversationServices.Application.Interfaces
{
    public interface IMessageFileService
    {
        Task<MessageFile> UploadFile(Guid messageId, IFormFile file);
        Task<byte[]> DownloadFile(Guid fileId);
        Task DeleteFile(Guid fileId);
        Task<List<MessageFile>> GetMessageFiles(Guid messageId);
    }
} 