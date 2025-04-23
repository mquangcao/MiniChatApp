using MiniChatApp.SDK.Services.MessageReader;
using MiniChatApp.Server.MessageReader;
using System.Net.Sockets;

namespace MiniChatApp.Server.Factory
{
    internal class ReadMessageFactory(ILoggerFactory loggerFactory) : IReadMessageFactory
    {
        public IMessageReader Create(Socket socket)
        {
            return new DefaultMessageReader(socket, loggerFactory.CreateLogger<DefaultMessageReader>());
        }
    }
}
