namespace MiniChatApp.SDK.Entities;

public class Message
{
    public required Guid Id { get; init; }
    public required Guid SenderId { get; init; }
    public required Guid ReceiverId { get; init; }
    public required string Content { get; init; }
    public required DateTime Timestamp { get; init; }

}
