using MiniChatApp.SDK.Entities;


namespace MiniChatApp.Server.Commands
{
    public abstract class ChatCommand
    {
        public abstract CommandType Command { get; }
        public abstract string Description { get; }
        public abstract string Usage { get; }
        public required Message Message { get; init; } = default!;
        public required string[] Args { get; init; } = default!;
        public abstract Task ExecuteAsync();
    }
}
