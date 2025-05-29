using Microsoft.AspNetCore.Http;

namespace BreezyDrive.ConversationServices.Application.DTOs.Requests
{
    public class MessageFileRequest
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public IFormFile File { get; set; }
    }
} 