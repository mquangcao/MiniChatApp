using MiniChatApp.SDK.Services.MessageReader;
using System.Net.Sockets;

namespace MiniChatApp.Server.Factory
{
    public interface IReadMessageFactory
    {
        IMessageReader Create(Socket socket);
    }
}
