using MiniChatApp.SDK.Entities;
using MiniChatApp.Server.Commands;

namespace MiniChatApp.Server.Builder
{
    internal class ChatCommandBuilder
    {
        public required CommandType Command { get; set; }
        public string Description { get; set; } = string.Empty;
        public required string Usage { get; set; } = string.Empty;
        public required Message Message { get; set; } = default!;
        public required string[] Args { get; set; } = default!;


    }
}
