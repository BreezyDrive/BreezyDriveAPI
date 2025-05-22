using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.ConversationServices.Application.Interfaces;
using BreezyDrive.ConversationServices.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Linq;

namespace BreezyDrive.ConversationServices.Application.Services
{
    public class MessageFileService : IMessageFileService
    {
        private readonly IMongoRepository<MessageFile> _messageFileRepository;
        private readonly IGridFSBucket _gridFS;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public MessageFileService(
            IMongoUnitOfWork unitOfWork,
            IMongoDatabase database,
            IConfiguration configuration)
        {
            _messageFileRepository = unitOfWork.Repository<MessageFile>("MessageFiles");
            _gridFS = new GridFSBucket(database);
            _configuration = configuration;
            _baseUrl = configuration["FileStorage:BaseUrl"] ?? "http://localhost:5000/api/ConversationMessage/DownloadFile/";
        }

        public async Task<MessageFile> UploadFile(Guid messageId, IFormFile file)
        {
            ValidateFile(file);

            // Upload file to GridFS
            var options = new GridFSUploadOptions
            {
                Metadata = new MongoDB.Bson.BsonDocument
                {
                    { "messageId", messageId.ToString() },
                    { "fileName", file.FileName },
                    { "contentType", file.ContentType },
                    { "fileSize", file.Length }
                }
            };

            var fileId = await _gridFS.UploadFromStreamAsync(
                file.FileName,
                file.OpenReadStream(),
                options);

            // Create MessageFile record
            var messageFile = new MessageFile
            {
                MessageId = messageId,
                FiledId = fileId.ToString(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                FileUrl = $"{_baseUrl}{fileId}"
            };

            await _messageFileRepository.InsertAsync(messageFile);
            return messageFile;
        }

        private void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new CustomExceptions.InvalidDataException("File is empty");

            // Get file size limits from configuration
            var maxFileSizeInMB = _configuration.GetValue<int>("FileStorage:FileSizeLimits:MaxFileSizeInMB", 10);
            var maxFileSizeInBytes = maxFileSizeInMB * 1024 * 1024;

            if (file.Length > maxFileSizeInBytes)
                throw new CustomExceptions.InvalidDataException($"File size exceeds maximum allowed size of {maxFileSizeInMB}MB");

            // Get allowed file types from configuration
            var allowedImageTypes = _configuration.GetSection("FileStorage:AllowedFileTypes:Images").Get<string[]>() ?? Array.Empty<string>();
            var allowedDocumentTypes = _configuration.GetSection("FileStorage:AllowedFileTypes:Documents").Get<string[]>() ?? Array.Empty<string>();
            var allowedTypes = allowedImageTypes.Concat(allowedDocumentTypes).ToArray();

            if (!allowedTypes.Contains(file.ContentType))
                throw new CustomExceptions.InvalidDataException($"File type {file.ContentType} is not allowed");
        }

        public async Task<byte[]> DownloadFile(Guid fileId)
        {
            try
            {
                var stream = await _gridFS.OpenDownloadStreamAsync(MongoDB.Bson.ObjectId.Parse(fileId.ToString()));
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            catch (GridFSFileNotFoundException)
            {
                throw new CustomExceptions.DataNotFoundException("File not found");
            }
        }

        public async Task DeleteFile(Guid fileId)
        {
            try
            {
                await _gridFS.DeleteAsync(MongoDB.Bson.ObjectId.Parse(fileId.ToString()));
                var messageFile = await _messageFileRepository.GetAllAsync();

                var checkmessageFile = messageFile
                    .Where(n => n.MessageId == fileId)
                    .FirstOrDefault();

                if (checkmessageFile != null)
                {
                    await _messageFileRepository.DeleteAsync(checkmessageFile.FiledId);
                }

            }
            catch (GridFSFileNotFoundException)
            {
                throw new CustomExceptions.DataNotFoundException("File not found");
            }
        }

        public async Task<List<MessageFile>> GetMessageFiles(Guid messageId)
        {
            var files = await _messageFileRepository.GetAllAsync();

            var fileLists = files
                .Where(f => f.MessageId == messageId)
                .ToList();

            return fileLists;
        }
    }
}