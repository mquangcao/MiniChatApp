using MiniChatApp.SDK.Entities;
using MiniChatApp.Server.Commands;

namespace MiniChatApp.Server.Factory
{
    public interface IChatCommandFactory
    {
        public ChatCommand CreateCommand(Message message);
    }
}
