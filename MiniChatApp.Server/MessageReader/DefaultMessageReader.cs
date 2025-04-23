using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Services.MessageReader;
using MiniChatApp.Server.Builder;
using MiniChatApp.Server.Utils;
using System.Net.Sockets;

namespace MiniChatApp.Server.MessageReader
{
    internal class DefaultMessageReader : IMessageReader
    {
        private readonly Socket _socket;
        private readonly ILogger<DefaultMessageReader> _logger;
        public DefaultMessageReader(Socket socket, ILogger<DefaultMessageReader> logger)
        {
            _socket = socket;
            _logger = logger;
        }

        /*
         * senderId: <senderId-Guid>\n
         * receiverId: <receiverId-Guid>\n
         * \n
         * <message>\n
         */
        public async Task<Message?> ReadMessageAsync(CancellationToken stoppingToken)
        {
            var stream = new NetworkStream(_socket);
            var reader = new StreamReader(stream);

            var sender = await reader.ReadLineAsync(stoppingToken);
            var receiver = await reader.ReadLineAsync(stoppingToken);
            var msgn = await reader.ReadLineAsync(stoppingToken); // empty line
            if (string.IsNullOrEmpty(sender) || string.IsNullOrEmpty(receiver))
            {
                _logger.LogWarning("Invalid message format");
                return null;
            }

            MessageBuilder messageBuilder = new();

            if (HeaderParser.TryParseHeader(sender!, out var senderId) && HeaderParser.TryParseHeader(receiver!, out var receiverId))
            {
                messageBuilder.SenderId = senderId;
                messageBuilder.ReceiverId = receiverId;
            }

            var messageContent = await reader.ReadLineAsync(stoppingToken);
            if (string.IsNullOrEmpty(messageContent) )
            {
                _logger.LogWarning("Message content is empty");
                return null;
            }
            messageBuilder.Content = messageContent;
            messageBuilder.Id = Guid.NewGuid();

            return messageBuilder.Build();
        }


    }
}
