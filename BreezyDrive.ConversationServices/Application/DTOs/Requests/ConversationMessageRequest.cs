namespace BreezyDrive.ConversationServices.Application.DTOs.Requests
{
    public class ConversationMessageRequest
    {
        public Guid SenderId { get; set; }

        public string Content { get; set; }

        public Guid? ReplyToMessageId { get; set; }
    }
}
