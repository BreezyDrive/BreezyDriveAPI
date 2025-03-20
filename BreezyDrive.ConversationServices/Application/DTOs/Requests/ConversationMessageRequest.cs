namespace BreezyDrive.ConversationServices.Application.DTOs.Requests
{
    public class ConversationMessageRequest
    {
        public Guid ConverationId { get; set; }

        public Guid SenderId { get; set; }

        public DateTime CreateTime { get; set; }

        public string Content { get; set; }

        public bool IsSeen { get; set; }

        public Guid ReplyToMessageId { get; set; }
    }
}
