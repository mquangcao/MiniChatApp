namespace MiniChatApp.Server.Models
{
    internal class ClientConnection
    {
        public required Guid Id { get; init; }
        public required Task HandleTask { get; init; }
    }
}
