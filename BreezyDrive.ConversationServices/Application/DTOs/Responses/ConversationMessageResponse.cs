using AutoMapper;
using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.ConversationServices.Domain.Entities;

namespace BreezyDrive.ConversationServices.Application.DTOs.Responses
{
    public class ConversationMessageResponse : IMapFrom<ConversationMessage>
    {
        public Guid ConversationId { get; set; }

        public Guid SenderId { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public string Content { get; set; }

        public bool IsSeen { get; set; }

        public Guid? ReplyToMessageId { get; set; }

        public List<MessageFile> Files { get; set; } = new List<MessageFile>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ConversationMessage, ConversationMessageResponse>();
        }
    }
}
