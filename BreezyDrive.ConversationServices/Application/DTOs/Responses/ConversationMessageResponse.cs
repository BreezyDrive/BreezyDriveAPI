using AutoMapper;
using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.ConversationServices.Domain.Entities;

namespace BreezyDrive.ConversationServices.Application.DTOs.Responses
{
    public class ConversationMessageResponse : IMapFrom<ConversationMessageResponse>
    {
        public Guid ConverationId { get; set; }

        public Guid SenderId { get; set; }

        public DateTime CreateTime { get; set; }

        public string Content { get; set; }

        public bool IsSeen { get; set; }

        public Guid ReplyToMessageId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ConversationMessage, ConversationMessageResponse>();
        }
    }
}
