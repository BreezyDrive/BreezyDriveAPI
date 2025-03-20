using AutoMapper;
using BreezyDrive.Common.Application.Mapper;
using BreezyDrive.ConversationServices.Domain.Entities;

namespace BreezyDrive.ConversationServices.Application.DTOs.Responses
{
    public class ConversationResponse : IMapFrom<Conversation>
    {
        public Guid UserId1 { get; set; }

        public Guid UserId2 { get; set; }

        public string LastMessage { get; set; }

        public bool IsOpen { get; set; }

        public Guid? CloseAccountId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Conversation, ConversationResponse>();
        }
    }
}
