using MiniChatApp.SDK.Entities;

namespace MiniChatApp.Server.Builder
{
    internal class MessageBuilder
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = default!;

        private void Validate()
        {
            if (Id == Guid.Empty)
            { 
                throw new ArgumentException("Id cannot be empty", nameof(Id)); 
            }
            if (SenderId == Guid.Empty)
            {
                throw new ArgumentException("SenderId cannot be empty", nameof(SenderId));
            }
            if (ReceiverId == Guid.Empty)
            {
                throw new ArgumentException("ReceiverId cannot be empty", nameof(ReceiverId));
            }
            if (string.IsNullOrEmpty(Content))
            {
                throw new ArgumentException("Content cannot be null or empty", nameof(Content));
            }
        }

        public Message Build()
        {
            Validate();
            return new Message
            {
                Id = Id,
                SenderId = SenderId,
                ReceiverId = ReceiverId,
                Content = Content,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
